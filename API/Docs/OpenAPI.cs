using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Docs
{
    public static class OpenAPI
    {
        public static readonly Version Version = new(3, 0, 45, 156);
        public static readonly OpenApiInfo Info = new()
        {
            Version = Version.ToString(),
            Title = "Quick Start",
            Description = "<p>Documentation API of <b>Text Mining Project</b> has been written by Live Laugh Love.</p><p><b><i>This is an internal resource. Do not share it in any way.</i></b></p><p>You should read carefully the descriptions, response status codes and arguments request belong to each API Resource. It take your time, but you will avoid a lot of bugs afterward.</p><b>The application case sensitive.</b>",
            Contact = new OpenApiContact
            {
                Name = "nhudangstudy, anhhoangdev, cocaa",
                Email = "nhucnh21416c@st.uel.edu.vn, anhth21416c@st.uel.edu.vn, hienttm21416c@st.uel.edu.vn"
            }
        };
        public static readonly OpenApiSecurityScheme SecurityScheme = new()
        {
            Name = HeaderNames.Authorization,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "The token to execute the private resources."
        };
        public static readonly OpenApiSecurityRequirement SecurityRequirement = new()
        {
            {
                new()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };

        public static void AutoMapping(this SwaggerGenOptions options)
        {
            options.MapType<TimeSpan>(() => new()
            {
                Type = "string",
                Example = new OpenApiString("00:45:00")
            });
        }
    }
}
