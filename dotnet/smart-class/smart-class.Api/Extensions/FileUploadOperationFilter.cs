//using Microsoft.AspNetCore.Http;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Collections.Generic;
//using System.Linq;

//namespace smart_class.Api.Extensions
//{
//    public class FileUploadOperationFilter : IOperationFilter
//    {
//        public void Apply(OpenApiOperation operation, OperationFilterContext context)
//        {
//            // בדיקה אם יש פרמטר מסוג IFormFile
//            var hasFormFile = context.MethodInfo.GetParameters()
//                .Any(p => p.ParameterType == typeof(IFormFile));

//            if (hasFormFile)
//            {
//                // יצירת RequestBody מותאם אישית עבור multipart/form-data
//                operation.RequestBody = new OpenApiRequestBody
//                {
//                    Content = new Dictionary<string, OpenApiMediaType>
//                    {
//                        ["multipart/form-data"] = new OpenApiMediaType
//                        {
//                            Schema = new OpenApiSchema
//                            {
//                                Type = "object",
//                                Properties = new Dictionary<string, OpenApiSchema>
//                                {
//                                    ["file"] = new OpenApiSchema
//                                    {
//                                        Type = "string",    // הגדרת הסוג כ-string
//                                        Format = "binary"   // הגדרת הפורמט כ-binary
//                                    }
//                                },
//                                Required = new HashSet<string> { "file" } // הגדרת "file" כפרמטר חובה
//                            }
//                        }
//                    }
//                };
//            }
//        }
//    }
//}
