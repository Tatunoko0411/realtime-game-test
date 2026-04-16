//using bicycle_racing.Server.StreamingHubs;
//using Microsoft.AspNetCore.Server.Kestrel.Core;
//using Microsoft.OpenApi.Models;
//using bicycle_racing.Server.StreamingHubs;

//var builder = WebApplication.CreateBuilder(args);

//// --- 1. Kestrel の設定 (Caddyとの通信に必須) ---
//builder.WebHost.ConfigureKestrel(options =>
//{
//    // 8080ポートで待受
//    options.ListenAnyIP(8080, listenOptions =>
//    {
//        // 重要: HTTP/2 専用に固定します。
//        // これにより Caddy からの h2c (暗号化なしHTTP/2) を正しく受け入れます。
//        listenOptions.Protocols = HttpProtocols.Http2;
//    });
//});

//// --- 2. サービス登録 ---
//builder.Services.AddSingleton<RoomContextRepository>();

//// MagicOnion の登録
//builder.Services.AddMagicOnion();

//// CORS設定
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
//    });
//});

//var app = builder.Build();

//// --- 3. パイプライン設定 (順番が重要) ---

//// ルーティングとCORSを先に適用
//app.UseRouting();
//app.UseCors("AllowAll");

//// ★重要★ app.UseGrpcWeb() は削除しました。
//// Unity側をHTTP/2専用にしたため、これがあると逆に通信を壊す原因になります。

//// MagicOnion サービスのマッピング
//app.MapMagicOnionService();

//// 疎通確認用のルート
//app.MapGet("/", () => "MagicOnion Server is running (HTTP/2)");

//app.Run();


///
/// 以下はローカルで動かす用
///

//using bicycle_racing.Server.StreamingHubs;
//using Microsoft.OpenApi.Models;
//var builder = WebApplication.CreateBuilder(args);
//var magiconion = builder.Services.AddMagicOnion();
//if (builder.Environment.IsDevelopment())
//{
//    magiconion.AddJsonTranscoding();
//    builder.Services.AddMagicOnionJsonTranscodingSwagger();
//}
//builder.Services.AddSwaggerGen(options =>
//{
//    options.IncludeMagicOnionXmlComments(Path.Combine(AppContext.BaseDirectory, "bicycle_racing.Shared.xml"));
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "bicycle_racing",
//        Description = "bicycle racing",
//    });
//});
//builder.Services.AddMvcCore().AddApiExplorer();
//builder.Services.AddSingleton<RoomContextRepository>();
//var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "bicycle_racing");
//    });
//}
//app.MapMagicOnionService();
//app.MapGet("/", () => "");

//app.Run();

using bicycle_racing.Server.StreamingHubs;
using Grpc.Core;
using Grpc.Core.Logging;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
var builder = WebApplication.CreateBuilder(args);
var magiconion = builder.Services.AddMagicOnion();
if (builder.Environment.IsDevelopment())
{
    magiconion.AddJsonTranscoding();
    builder.Services.AddMagicOnionJsonTranscodingSwagger();
}
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeMagicOnionXmlComments(Path.Combine(AppContext.BaseDirectory, "bicycle_racing.Shared.xml"));
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "bicycle_racing",
        Description = "説明",
    });
});
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddSingleton<RoomContextRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "bicycle_racing");
    });
}
app.MapMagicOnionService();
app.MapGet("/", () => "");

GrpcEnvironment.SetLogger(new ConsoleLogger());

app.Run();
