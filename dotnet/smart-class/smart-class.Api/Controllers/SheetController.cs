//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using smart_class.Core.DTOs;
//using smart_class.Core.Services;
//using OpenAI;
//using OpenAI.Managers;
//using OpenAI.ObjectModels;
//using OpenAI.ObjectModels.RequestModels;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace smart_class.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SheetController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        private readonly IConfiguration _configuration;

//        public SheetController(IMapper mapper, IConfiguration configuration)
//        {
//            _mapper = mapper;
//            _configuration = configuration;
//        }

//        // Endpoint המאפשר למשתמש לשלוח נושא ולקבל תוכן מותאם אישית
//        [HttpPost("GenerateCustomContent")]
//        public async Task<ActionResult<string>> GenerateCustomContent([FromBody] string topic)
//        {
//            if (string.IsNullOrEmpty(topic))
//                return BadRequest("Topic cannot be null or empty.");

//            // קבלת מפתח ה-API מ-configurations
//            var apiKey = _configuration["OpenAI:ApiKey"];
//            var service = new OpenAIService(new OpenAiOptions
//            {
//                ApiKey = apiKey
//            });

//            // יצירת שאלה מותאמת עבור OpenAI
//            var request = new ChatCompletionCreateRequest
//            {
//                Model = Models.Gpt_4o_mini,
//                Messages = new List<ChatMessage>
//                {
//                    ChatMessage.FromSystem("המשתמש ביקש שכפול מותאם אישית בנושא הבא:"),
//                    ChatMessage.FromUser($"הנה נושא: {topic}")
//                },
//                Temperature = 0.7f // תמציא תשובה יצירתית יותר
//            };

//            // בקשה ל-OpenAI לקבלת התשובה
//            var result = await service.ChatCompletion.CreateCompletion(request);

//            if (result.Successful)
//            {
//                var content = result.Choices.First().Message.Content;
//                return Ok(content);
//            }
//            else
//            {
//                return StatusCode(500, $"Error generating content: {result.Error?.Message}");
//            }
//        }
//    }
//}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using smart_class.Core.DTOs;
using smart_class.Core.Services;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Properties;
using System.IO;
using iText.Kernel.Font;
using iText.IO.Font;

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public SheetController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        // Endpoint המאפשר למשתמש לשלוח נושא ולקבל תוכן מותאם אישית עם כותרת ומסגרת בגודל A4
        [HttpPost("GenerateCustomContent")]
        public async Task<ActionResult> GenerateCustomContent([FromBody] string topic)
        {
            // אם הנושא ריק או null, מחזירים תשובה של שגיאה
            if (string.IsNullOrEmpty(topic))
                return BadRequest("Topic cannot be null or empty.");

            try
            {
                // קבלת מפתח ה-API מ-configurations
                var apiKey = _configuration["OpenAI:ApiKey"];
                var service = new OpenAIService(new OpenAiOptions
                {
                    ApiKey = apiKey
                });

                // יצירת שאלה מותאמת עבור OpenAI
                var request = new ChatCompletionCreateRequest
                {
                    Model = Models.Gpt_4o_mini,
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("המשתמש ביקש שכפול מותאם אישית בנושא הבא:"),
                        ChatMessage.FromUser($"הנה נושא: {topic}")
                    },
                    Temperature = 0.7f // תמציא תשובה יצירתית יותר
                };

                // בקשה ל-OpenAI לקבלת התשובה
                var result = await service.ChatCompletion.CreateCompletion(request);

                if (result.Successful)
                {
                    var content = result.Choices.First().Message.Content;

                    // יצירת PDF בגודל A4
                    using (var ms = new MemoryStream())
                    {
                        // יצירת כותב PDF שיכתוב ל-Stream
                        using (var writer = new PdfWriter(ms))
                        {
                            using (var pdf = new PdfDocument(writer))
                            {
                                var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);
                                document.SetMargins(20, 20, 20, 20); // הגדרת שוליים

                                // הגדרת הפונט
                                var fontPath = @"C:\path\to\your\hebrew-font.ttf"; // הקפד להחליף את הנתיב לפונט העברי שלך
                                var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                                // כותרת עם פונט עברי
                                var title = new Paragraph($"נושא: {topic}")
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetFont(font) // הגדרת הפונט בעברית
                                    .SetFontSize(16)
                                    .SetBold();
                                document.Add(title);

                                // תוכן המתקבל מ-OpenAI עם פונט עברי
                                var contentParagraph = new Paragraph(content)
                                    .SetTextAlignment(TextAlignment.LEFT)
                                    .SetFont(font) // הגדרת הפונט בעברית
                                    .SetFontSize(12);
                                document.Add(contentParagraph);

                                // סגירת ה-PDF ושמירתו ב-Stream
                                document.Close();
                            }
                        }

                        // מחזירים את ה-PDF כקובץ
                        return File(ms.ToArray(), "application/pdf", "GeneratedContent.pdf");
                    }
                }
                else
                {
                    // אם התשובה לא הצליחה, מחזירים שגיאה עם הודעת השגיאה
                    return StatusCode(500, $"Error generating content: {result.Error?.Message}");
                }
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות כלליות אם יש בעיה בהגשת הבקשה ל-OpenAI או בעיות אחרות
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
