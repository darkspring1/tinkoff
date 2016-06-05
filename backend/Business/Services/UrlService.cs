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

        private Url Create(string shortUrl, string originUrl)
        {
            var url = new Url
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ShortUrl = shortUrl,
                OriginUrl = originUrl
            };
            _urlRepository.Add(url);
            _urlRepository.SaveChanges();
            return url;
        }


        public Url GetById(Guid id)
        {
            return _urlRepository.GetById(id);
        }

        public IEnumerable<Url> GetOrCreate(string origin, string shortUrlPart)
        {
            origin = origin.TrimEnd('/');
            const int maxFails = 2;
            var length = PathLength;
            for (var fails = 0; fails <= maxFails; fails++)
            {
                var urls = GetExisting(origin);
                if (urls.Any())
                {
                    return urls;
                }
                try
                {
                    var url = Create(shortUrlPart + "/" + _stringGenerator.GetString(length), origin);
                    return new[] { url };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    //обрабатываем ситуацию, когда сгенерили не уникальный shortUrl
                    //или пытаемся добавить запись для уже существующего originUrl'а
                    //моё предположение, что это будет проиходить редко,
                    //поэтому решил обойтись без lock, и дополнительных проверок
                    length++;   
                }
            }
            return Enumerable.Empty<Url>();
        }

        public Url AddTraffic(string shortUrl)
        {
            var result = _urlRepository.Where(url => url.ShortUrl == shortUrl).FirstOrDefault();
            if (result != null)
            {
                result.Traffic++;
                _urlRepository.SaveChanges();
            }

            return result;
        }
    }
}
