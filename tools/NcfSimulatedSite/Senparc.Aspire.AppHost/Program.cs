using Aspire.Hosting;
using System.Text;

var builder = DistributedApplication.CreateBuilder(args);

//if (builder.ExecutionContext.IsRunMode)
//{
var installer = builder.AddProject("installer", "../Senparc.Xncf.Installer/Senparc.Xncf.Installer.csproj")
         .WithExternalHttpEndpoints();

//builder.AddContainer("my-redis", "redis", "5.0.14");
//}


var ncfWeb = builder.AddProject<Projects.Senparc_Web>("ncf-simulate-web")
           .WithReference(installer)
           .WithExternalHttpEndpoints();


//֧�������ַ�
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.GetEncoding("GB2312");

builder.Build().Run();
