<img src="https://i.imgur.com/qA5ubgH.png" align="right"
     title="Frontier Elite Dangerous API" width="280">
# Frontier Elite Dangerous API
![Nuget](https://img.shields.io/nuget/v/NSW.EliteDangerous.INARA?label=nuget%3Astable)![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/NSW.EliteDangerous.INARA?label=nuget%3Adev)![Nuget](https://img.shields.io/nuget/dt/NSW.EliteDangerous.INARA)

![GitHub top language](https://img.shields.io/github/languages/top/h0useRus/EliteDangerousINARA)![GitHub](https://img.shields.io/github/license/h0useRus/EliteDangerousINARA)![GitHub Release Date](https://img.shields.io/github/release-date/h0useRus/EliteDangerousINARA)![GitHub last commit](https://img.shields.io/github/last-commit/h0useRus/EliteDangerousINARA)
## Intro
Simple library for [INARA](https://inara.cz/) integration based on [documentation](https://inara.cz/inara-api/).

## Usage

Use `AddEliteDangerousINARA()` method to add API into your app:
```c#
// Use Microsoft.Extensions.DependencyInjection
var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel=LogLevel.Debug)
                .AddEliteDangerousAPI()
                .AddEliteDangerousINARA()
                .BuildServiceProvider();
```
