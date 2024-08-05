using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.UnitTestExtension;
using Senparc.Ncf.UnitTestExtension.Entities;
using Senparc.Xncf.DynamicData;
using Senparc.Xncf.DynamicData.Domain.Models.DatabaseModel.Dto;
using Senparc.Xncf.DynamicData.Domain.Services;

namespace Senparc.Xncf.DynamicDataTests
{
    public class BaseDynamicDataTest_Seed : UnitTestSeedDataBuilder
    {
        private static object InitLock = new object();
        private static bool InitFinished = false;

        public override async Task ExecuteAsync(IServiceProvider serviceProvider, DataList dataList)
        {
            lock (InitLock)
            {
                if (InitFinished)
                {
                    //���ڵ�Ԫ����ÿ�� TestMethod �������³�ʼ�� TestClass �࣬�����Ҫ��ֹ��̬��������ݱ��ظ���ӡ�
                    return;
                }

                // TableMetadata
                List<TableMetadata> tableMetadataList = new() {
                     new("User","�û���"){
                          ColumnMetadatas=new List<ColumnMetadata>(){
                               new ColumnMetadata(0,"Guid","Text",false,""),
                               new ColumnMetadata(0,"UserName","Text",false,""),
                               new ColumnMetadata(0,"Balance","Float",false,"0.0"),
                          }
                         },
                         new("Product","��Ʒ��"){
                          ColumnMetadatas = new List<ColumnMetadata>(){
                               new ColumnMetadata(0,"Guid","Text",false,""),
                               new ColumnMetadata(0,"Name","Text",false,""),
                               new ColumnMetadata(0,"Price","Float",false,"0.0"),
                          }
                         },
                         new("Order","������"){
                          ColumnMetadatas = new List<ColumnMetadata>(){
                               new ColumnMetadata(0,"Guid","Text",false,""),
                               new ColumnMetadata(0,"UserGuid","Text",false,""),
                               new ColumnMetadata(0,"ProductGuid","Text",false,""),
                               new ColumnMetadata(0,"Price","Float",false,"0.0"),
                               new ColumnMetadata(0,"State","Enums(Open,Paid,Closed)",false,"0.0"),
                          }
                     },
                };

                dataList.Add(tableMetadataList);
                InitFinished = true;
            }
        }

        public override async Task OnExecutedAsync(IServiceProvider serviceProvider, DataList dataList)
        {
            var tableDataService = serviceProvider.GetRequiredService<TableDataService>();
            var  columnMetadataService = serviceProvider.GetRequiredService<ColumnMetadataService>();

            // User ��
            var userTableColumns = await columnMetadataService.GetColumnDtos(1);
            var tableDataDtos = new List<TableDataDto>();

            foreach (var column in userTableColumns)
            {
                tableDataDtos.Add(new TableDataDto() { 
                 TableId = column.TableMetadataId,
                  ColumnMetadataId = column.Id,
                   
                });
            }
        }
    }

    [TestClass]
    public class BaseDynamicDataTest : BaseNcfUnitTest
    {
        public BaseDynamicDataTest(Action<IServiceCollection> servicesRegister = null, UnitTestSeedDataBuilder seedDataBuilder = null)
     : base(servicesRegister, seedDataBuilder ?? new BaseDynamicDataTest_Seed())
        {

        }

        protected override void BeforeRegisterServiceCollection(IServiceCollection services)
        {
            base.BeforeRegisterServiceCollection(services);

            Console.WriteLine("BaseDynamicDataTest.BeforeRegisterServiceCollection");
        }

        protected override void RegisterServiceCollectionFinished(IServiceCollection services)
        {
            base.RegisterServiceCollectionFinished(services);
        }
    }
}