<img src="https://i.imgur.com/qA5ubgH.png" align="right"
     title="Frontier Elite Dangerous INARA" width="280">
# Frontier Elite Dangerous INARA
![Nuget](https://img.shields.io/nuget/v/NSW.EliteDangerous.INARA?label=nuget%3Astable)![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/NSW.EliteDangerous.INARA?label=nuget%3Adev)![Nuget](https://img.shields.io/nuget/dt/NSW.EliteDangerous.INARA)

![GitHub top language](https://img.shields.io/github/languages/top/h0useRus/EliteDangerousINARA)![GitHub](https://img.shields.io/github/license/h0useRus/EliteDangerousINARA)![GitHub Release Date](https://img.shields.io/github/release-date/h0useRus/EliteDangerousINARA)![GitHub last commit](https://img.shields.io/github/last-commit/h0useRus/EliteDangerousINARA)
## Intro
Simple library for [INARA](https://inara.cz/) integration based on [documentation](https://inara.cz/inara-api/).

## Usage

Use `AddEliteDangerousINARA()` method to add API into your app:
```c#
// Use Microsoft.Extensions.DependencyInjection
 var inara = new ServiceCollection()
                .AddLogging(cfg => cfg.AddDebug())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Warning)
                .AddEliteDangerousINARA(o =>
                {
                    o.ApiKey = "your_apikey_from_inara";
                    o.ApplicationName = "your_application_name";
                    o.ApplicationVersion = "1.2.3";
                    o.IsDevelopment = true; // set this while you debugging
                })
                .BuildServiceProvider()
                .GetService<IEliteDangerousINARA>();
```
or with known Commander
```c#
// Use Microsoft.Extensions.DependencyInjection
 var inara = new ServiceCollection()
                .AddLogging(cfg => cfg.AddDebug())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Warning)
                .AddEliteDangerousINARA(o =>
                {
                    o.ApiKey = "your_apikey_from_inara";
                    o.ApplicationName = "your_application_name";
                    o.ApplicationVersion = "1.2.3";
                    o.Commander = "Your Commander";
                    o.FrontierId = "F1234567"; // this optional
                })
                .BuildServiceProvider()
                .GetService<IEliteDangerousINARA>();
```

