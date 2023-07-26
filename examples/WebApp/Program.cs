using ASKit.Common.Mail;
using ASKit.Mail.MailKit;
using ASKit.Mail.Extensions.DependencyInjection;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ess;
using System.Text;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSkMailService(builder.Configuration);

var app = builder.Build();

// Testing mail service 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var mailService = services.GetRequiredService<ISkMailService>();
    
    await mailService.SendEmailAsync(new SkMailMessage("Admin <unknown@gmail.com>, supervisor@domen", 
        "Test", "<h3>Body message</h3>"));

    byte[] myTextBytes = Encoding.ASCII.GetBytes("<h1>Hello, world</h1>");
    var attachments = new List<SkAttachment> { 
        new SkAttachment("appsettings.json"),
        new SkAttachment("MyFile.html", 
                        (new ContentType { MediaType = MediaTypeNames.Text.Html }).ToString(),
                        myTextBytes)
    };
    await mailService.SendEmailAsync(new SkMailMessage("Admin <unknown@gmail.com>",
        "Test with attachment", "<h3>Body message</h3>", attachments));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();



app.Run();
