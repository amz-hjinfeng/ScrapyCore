﻿using Microsoft.AspNetCore.Mvc;
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
using ScrapyCore.Fundamental.Kernel.Load;
using ScrapyCore.Core;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{

    [Route("api/[controller]")]
    [ApiController]
    public class ScrapyMetaController : Controller
    {
        private static IReadOnlyDictionary<string, SourceGenAttribute> SourceGenMeta => SourceGenManager.SourceGenMeta;
        private static IReadOnlyDictionary<string, ConvertorManager.ConvertorMeta> ConvertorMetas => ConvertorManager.ConvertorMetas;
        private static IReadOnlyDictionary<string, LoadSuites> LoadProviderMetas = LoadProviderManager.LoadMetas;

        private static IReadOnlyDictionary<string, string> LoadProviderTypesFieldMapping => new Dictionary<string, string>()
        {
            {"Storage","StorageType" },
            {"MessageQueue","MessageQueueEngine"},
            { "ElasticSearch","ElasticSearhEngine"},
            { "Schedule","Type"}
        };

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



        [HttpGet]
        [Route("load-provider")]
        public ActionResult GetLoadMetas()
        {
            var result = LoadProviderManager.LoadMetas.Select(x =>
                new
                {
                    CategoryName = x.Key,
                    Metas = x.Value.ConfigureFactory.GetType()
                        .GetInterfaces()[0]
                        .GenericTypeArguments[0]
                        .GetProperties().Select(p => new
                        {
                            Name = p.Name,
                            Type = p.PropertyType.Name
                        }),
                    Services = (x.Value.ServiceFactory as IServiceKeys).GetServiceKeys().Select(q => q.Replace("Storage", "")),  //Temp replace here.
                    MappingTo = LoadProviderTypesFieldMapping[x.Key]

                }
            ).ToList();

            return Json(result);
        }
    }
}
