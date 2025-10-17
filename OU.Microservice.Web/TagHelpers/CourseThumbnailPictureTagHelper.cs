﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using OU.Microservice.Web.Options;

namespace OU.Microservice.Web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
    {
        public string? Src { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            var blankCourseThumbnailImagePath = "/images/blank_course.png";

            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var courseThumbnailImagePath = $"{microserviceOption.File.BaseAddress}/{Src}";


                output.Attributes.SetAttribute("src", courseThumbnailImagePath);
            }


            return base.ProcessAsync(context, output);
        }
    }
}
