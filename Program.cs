using Gossip.Models;
using Gossip.Services;
using Gossip.Services.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GossipDbContext>(options => {
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});

builder.Services.AddTransient<IProfilesService, EFProfileService>();

var app = builder.Build();

app.Run();