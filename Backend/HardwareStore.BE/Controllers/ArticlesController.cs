using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.BE.Services;
using HardwareStore.BE.Entities;
using System.Text.RegularExpressions;
using HardwareStore.BE.Models.Article;
using Microsoft.AspNetCore.Authorization;

namespace HardwareStore.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;
        public ArticlesController(IArticleService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [AllowAnonymous]
        [HttpGet("getArticles")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesAsync(bool isDescending)
        {
            IEnumerable<Article> articles = await _service.GetArticles(isDescending);
            if (articles.Any()) 
            {
                IEnumerable<ArticleDto> parsedArticles = articles.Select(article => new ArticleDto
                {
                    Id = article.Id,
                    ArticleName = article.ArticleName,
                    ArticleDescription = article.ArticleDescription,
                    ArticleType = article.ArticleType,
                    ArticlePrice = article.ArticlePrice,
                });


                return parsedArticles.Any() ? Ok(parsedArticles) : NoContent();
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesSearch([FromQuery] string query, bool isDescending = false)
        {
            IEnumerable<Article> articles = await _service.GetArticles(isDescending);
            if (articles.Any() && !String.IsNullOrEmpty(query))
            {
                string regexPattern = String.Join(" ", query.Split(" ")
                    .Select(w => String.Format("({0})|", w)));

                regexPattern = regexPattern.Remove(regexPattern.Length - 1);

                var articlesMapped = articles.Where(art => 
                {
                    string articleKeywords = String.Format("{0} {1}", art.ArticleName, art.ArticleType);
                    return Regex.IsMatch(articleKeywords, regexPattern, RegexOptions.IgnoreCase);
                }).Select(art => new ArticleDto()
                {
                    Id = art.Id,
                    ArticleName = art.ArticleName,
                    ArticleDescription = art.ArticleDescription,
                    ArticleType = art.ArticleType,
                    ArticlePrice = art.ArticlePrice,
                });

                return Ok(articlesMapped);
            }

            return NoContent();

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            try
            {
                Article article = await _service.GetArticle(id);
                if (article != null)
                {
                    return Ok(new ArticleDto()
                    {
                        Id = article.Id,
                        ArticleName = article.ArticleName,
                        ArticleDescription = article.ArticleDescription,
                        ArticleType = article.ArticleType,
                        ArticlePrice = article.ArticlePrice,
                    });
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }

            return NoContent();
        }


        [Authorize]
        [HttpPost("addArticle")]
        public async Task<ActionResult> AddArticle([FromBody] ArticleAddDto article)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            Article articleToAdd = new Article()
            {
                ArticleName = article.ArticleName,
                ArticleDescription = article.ArticleDescription,
                ArticleType = article.ArticleType,
                ArticlePrice = article.ArticlePrice,
            };

            if(await _service.IsItemAdded(articleToAdd))
                return Ok($"Article {articleToAdd.ArticleName} already exists!");

            try
            {
                await _service.PublishArticle(articleToAdd);
                return Ok("Article published successfully!");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.ToString());
            }
        }



        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            Article findingArticle = await _service.GetArticle(id);
            if(findingArticle == null)
            {
                return NotFound();
            }

            await _service.DeleteArticle(findingArticle);
            return Ok("Article deleted successfully!");
        }
    }
}
