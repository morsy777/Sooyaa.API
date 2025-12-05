global using System.Text;
global using System.Reflection;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;
global using System.ComponentModel.DataAnnotations;
global using System.Security.Cryptography;
global using LanguageApp.DTOS;
global using System.ComponentModel.DataAnnotations.Schema;


global using Mapster;
global using MapsterMapper;

global using FluentValidation;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Cors;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;

global using LanguageApp;
global using LanguageApp.Abstractions;
global using LanguageApp.Authentication;
global using LanguageApp.Abstractions.Consts;
global using LanguageApp.Contracts.Authentication;
global using LanguageApp.Contracts.User;
global using LanguageApp.Entities;
global using LanguageApp.Errors;
global using LanguageApp.Extensions;
global using LanguageApp.Services;
global using LanguageApp.Settings;
global using LanguageApp.Helpers;
global using LanguageApp.Persistence; 

global using MimeKit;
global using MailKit;
global using MailKit.Net.Smtp;
global using MailKit.Security;

global using Hangfire;
global using HangfireBasicAuthenticationFilter;



