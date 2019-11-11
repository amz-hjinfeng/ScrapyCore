using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core.Attributes;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Extract.Attributes;
using ScrapyCore.Fundamental.Scheduler.Attributes;
using ScrapyCore.Fundamental.Scheduler.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ScrapyCore.Fundamental.Kernel.Convertors;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{

    [Route("api/[controller]")]
    [ApiController]
    public class ScrapyMetaController : Controller
    {
        private static IReadOnlyDictionary<string, SourceGenAttribute> SourceGenMeta => SourceGenManager.SourceGenMeta;
        public static IReadOnlyDictionary<string, ConvertorManager.ConvertorMeta> ConvertorMetas => ConvertorManager.ConvertorMetas;
        /// <summary>
        /// api/scrapymeta/source-gens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("source-gens")]
        public ActionResult GetSourceGens()
        {
            return Json(new
            {
                Count = SourceGenMeta.Count,
                Metas = SourceGenMeta.Select(x =>
                new
                {
                    MetaName = x.Key
                })
            });
        }

        [HttpGet]
        [Route("source-gens/{name}")]
        public ActionResult GetSourceGens(string name)
        {
            if (SourceGenMeta.ContainsKey(name))
            {
                SourceGenAttribute sourceGenAttribute = SourceGenMeta[name];
                var props = sourceGenAttribute.ParameterType.GetProperties();
                return Json(new
                {
                    Count = props.Length,
                    props = props.Select(x =>
                    {
                        var fieldmeta = x.GetCustomAttribute<FieldAttribute>();
                        return new
                        {
                            FieldName = fieldmeta.FieldName,
                            AcceptType = fieldmeta.AcceptTypeName,
                            fieldmeta.DefaultValue,
                        };
                    }).ToArray()
                });
            }
            return NotFound();
        }

        /// <summary>
        /// api/scrapymeta/convertors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("convertors")]
        public ActionResult GetConvertors()
        {
            return Json(ConvertorMetas.Values.Select(x => new
            {
                ConventorName = x.Attribute.Name,
                ConstructorType = x.Attribute.ConstructorType?.Name
            }));
        }
    }
}
