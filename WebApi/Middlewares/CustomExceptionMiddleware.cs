using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{

    /* Middleware bir nevi request ve post işlemlerini izleme de diyebiliriz. */
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /* Invoke metodu middleware call edildiğinde execute edilecek olan metottur.Bu metot; end-poit'e gelen request'i ve end-point'in return ettiği response'a müdahale edebilmemizi sağlar.  */
        public async Task Invoke(HttpContext context)
        {

            //Döneceğimiz response'ı yazdık.
            var watch = Stopwatch.StartNew(); //Bu watch start ve stop ile request geldikten sonra response cıkana kadar geçen süreyi yani bu servis içinde ne kadar süre harcandı bunu görebiliyoruz

            try
            {
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);

            await _next(context);
            watch.Stop();
            
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
            Console.WriteLine(message);

            }
            catch (Exception ex)
          
            {
                watch.Stop();
                await HandleException(context,ex,watch);
            }
           
        }

        /* Bu aşağıdaki metot sayesinde swagger da API'yi test ettiğimizde yaptığımız hatanın ne olduğu hespi oradaki ekranda gözükecek.Burda bunu yaparak Controller'daki try catch'lerden kurtulduk. */
        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {

            string message = "[Error]  HTTP " + context.Request.Method + " - " + context.Response.StatusCode + "Error Message" + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            Console.WriteLine(message);

            context.Response.ContentType ="application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new {error = ex.Message} ,Formatting.None);
            return context.Response.WriteAsync(result);

        }


    }

     public static class CustomExceptionMiddlewareExtension
     {

         public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
         {
             return builder.UseMiddleware<CustomExceptionMiddleware>();
             //Şimdi bunu program.cs içerisine alıcam.
         }

     }   


}

//CQRS pattern'i kullanıyor bu proje