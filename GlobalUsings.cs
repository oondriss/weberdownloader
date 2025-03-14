﻿global using Hangfire;
global using Hangfire.MemoryStorage;
global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using TestApp.DbModels;
global using TestApp.Extensions;
global using TestApp.HangFire;
global using TestApp.Services.CronValidator;
global using TestApp.Services.IpValidator;
global using TestApp.Services.JobManager;
global using TestApp.Services.WeberReader;
global using TestApp.Services.DbManager;
global using TestApp.Services.WeberReader.VarComm;