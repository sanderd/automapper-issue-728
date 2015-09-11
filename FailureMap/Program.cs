using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.QueryableExtensions;
using FailureMap.ApiModel;
using FailureMap.DbModel;
using FailureMap.EntityFramework;

namespace FailureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SampleContext())
            {
                var configurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

                configurationStore.CreateMap<User, ApiUser>()
                    .IgnoreAllUnmapped()
                    .ForMember(api => api.Username, conf => conf.MapFrom(u => u.Username))
                    .ForMember(api => api.Documents, conf => conf.MapFrom(u => u.Documents));

                configurationStore.CreateMap<Document, ApiDocument>()
                    .IgnoreAllUnmapped()
                    .ForMember(api => api.Title, conf => conf.MapFrom(d => d.Title))
                    .ForMember(api => api.Meta, conf => conf.MapFrom(d => d));

                configurationStore.CreateMap<Document, ApiMeta>()
                    .ForMember(api => api.Owner, conf => conf.MapFrom(d => d.Owner.Username))
                    .ForMember(api => api.IsPublic, conf => conf.MapFrom(d => d.IsPublic));

                var mapper = new MappingEngine(configurationStore);

                var testUsingManualProjection = db.Users
                    .Select(u => new ApiUser
                    {
                        Documents = u
                            .Documents
                            .Select(d =>
                                new ApiDocument {Title = d.Title, Meta = new ApiMeta {IsPublic = d.IsPublic, Owner = d.Owner.Username}}
                            ),
                        Username = u.Username
                    })
                    .ToList();

                var testUsingAutomapper = db.Users
                    .Project(mapper)
                    .To<ApiUser>()
                    .ToList();

                testUsingAutomapper.ForEach(user => user.Documents.ToList().ForEach(doc => Console.WriteLine(doc.Title + " ispublic " + doc.Meta.IsPublic)));

                Console.ReadKey();
            }
        }
    }
}
