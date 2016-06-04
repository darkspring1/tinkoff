using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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


        private Url[] GetExisting(string origin)
        {
            return _urlRepository
                .Where(url => url.OriginUrl == origin).ToArray();
        }


        public IEnumerable<Url> GetOrCreate(string origin, string shortUrlPart)
        {
            origin = origin.TrimEnd('/');
            var urls = GetExisting(origin);
            if (!urls.Any())
            {
                var length = PathLength;
                bool fail = true;
                Url url = null;
                while (fail)
                {
                    try
                    {
                        url = new Url
                        {
                            Id = Guid.NewGuid(),
                            CreatedAt = DateTime.UtcNow,
                            ShortUrl =  shortUrlPart + _stringGenerator.GetString(length),
                            OriginUrl = origin
                        };
                        _urlRepository.Add(url);
                        _urlRepository.SaveChanges();
                        fail = false;
                    }
                    catch (Exception exc)
                    {
                        length++;
                        //обрабатываем ситуацию, когда сгенерили не уникальный shortUrl
                    }
                }
                urls = new[] {url};
            }
            return urls;
        }

        public IEnumerable<Url> GetByShortUrl(string shortUrl)
        {
            return _urlRepository.Where(url => url.ShortUrl == shortUrl).ToArray();
        }
    }
}
