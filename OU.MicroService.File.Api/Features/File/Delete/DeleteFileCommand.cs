﻿using OU.Microservice.Shared;

namespace OU.MicroService.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}
