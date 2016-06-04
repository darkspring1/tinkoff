using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dal;
using Business.Entities;

namespace Business.Services
{
    public class UrlService
    {
        private const int PathLength = 5;
        private IRepository<Url> _urlRepository;
        private readonly RandomStringGenerator _stringGenerator;
        public UrlService(IRepository<Url> urlRepository, RandomStringGenerator stringGenerator)
        {
            _urlRepository = urlRepository;
            _stringGenerator = stringGenerator;
        }


        private Url GetExisting(string origin)
        {
            return _urlRepository
                .Where(url => url.Origin == origin)
                .FirstOrDefault();
        }


        public Url Create(string origin)
        {
            origin = origin.TrimEnd('/');
            Url url = GetExisting(origin);
            if (url == null)
            {
                var length = PathLength;
                bool fail = true;
                while (fail)
                {
                    try
                    {
                        url = new Url
                        {
                            Id = Guid.NewGuid(),
                            CreatedAt = DateTime.UtcNow,
                            Path = _stringGenerator.GetString(length),
                            Origin = origin
                        };
                        _urlRepository.Add(url);
                        fail = false;
                    }
                    catch (Exception exc)
                    {
                        length++;
                        //обрабатываем ситуацию, когда сгенерили не уникальный path
                    }
                }
            }
            return url;
        }

        public IEnumerable<Url> GetByPath(string path)
        {
            return _urlRepository.Where(url => url.Path == path);
        }
    }
}
