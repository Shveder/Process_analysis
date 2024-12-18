﻿global using System.Net;
global using System.Security.Authentication;
global using Application.DTO;
global using Application.Exceptions;
global using Serilog;
global using Serilog.Events;
global using Infrastructure.DatabaseContext;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using System.Text.Json;
global using Application.Interfaces;
global using Application.Mappings;
global using Application.Services;
global using Infrastructure.Repository;
global using Microsoft.Extensions.FileProviders;
global using Web.Middlewares;
global using Common;
global using Application.Services.Base;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Core.Models;
global using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Tests")]