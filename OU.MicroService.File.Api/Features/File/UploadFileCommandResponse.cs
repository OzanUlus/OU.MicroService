﻿namespace OU.MicroService.File.Api.Features.File
{
    public record UploadFileCommandResponse(string FileName, string FilePath, string OriginalFileName);
}
