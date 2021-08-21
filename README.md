# Outdoor Activity Weather Tracker Website
##### Cadence Sweetser

## Project summary ##
Developed and run in Visual Studio, this is a C# program used to check the weather anywhere in the United States. Displays the current weather for any given location using the 
[OpenWeatherMap API](https://openweathermap.org/guide), and can look back at the last search for any previously searched areas.

## Major Tasks and Plans ##
***User Interface***

- Implementing a forecast tracker for 1-3 days ahead can be important for skiers interested in what the upcoming weather holds. Anything past 3 days can be deemed unreliable since weather in the mountains can change rapidly.

- Have a list of ski areas available through a "quick search" option.

***Database***

- Because there will be updates and edits to the site, so the database must be flexible.

### Tools to be used in future updates ###
- Azure Web App Service
- Convert the project to .NET Core instead of Framework 4.x, since Microsoft's support for Framework will soon be dropped.

## Installation and Usage ##
This C#, JavaScript, and SQL code is run in Visual Studio, so if need make sure C# and SQL is installed.

To set up your database for this project, create a database named "SkiWeather" and run the code below in your SQLCMD, or create a query in Microsoft SQL Server Management Studio with the code below and execute it.

Additionally, you must change the code on line 14 of the _Web.config_ file so that the Data Source is set to the name of your SQL Server's name.

```
USE [SkiWeather]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE[dbo].[WeatherHistory]
(

   [ResortCity] [varchar](50) NULL,
	[ResortDescription] [varchar] (50) NULL,
	[ResortTemp] [float] NULL,
	[ResortTempMin] [float] NULL,
	[ResortTempMax] [float] NULL,
	[ResortTime] [varchar] (50) NULL,
	[ResortLatitude] [float] NULL,
	[ResortLongitude] [float] NULL,
	[ResortHumidity] [int] NULL,
	[ResortCurrentWeather] [varchar] (50) NULL,
	[ResortTempFeels] [float] NULL
) ON[PRIMARY]
GO
```

When run, click the run button in Visual Studio, which should say "IIS Express (_Browser Here_)" next to it. 

## How to run the test cases:
In the test directory, the file _test.js_ contains two tests that use TestCafe. In order to use TestCafe, you must run ```npm install -g testcafe``` in your terminal. 

[Getting Started with TestCafe](https://devexpress.github.io/testcafe/documentation/getting-started/)

Here are a couple of ways to use TestCafe in the terminal for this project:
```
testcafe edge test.js
testcafe edge test.js -t "Weather Fetch Test"
```

## Sample Usages:
### Before anything happens

<img src = "https://raw.githubusercontent.com/chancesweetser/Weather-Tracker/master/assets/WeatherSearch1.png?token=AAMAN6EKUKFTQJ5ASERURJK7WWE52" width = "600"> <br>

### After searching for the weather in Centennial, WY

<img src = "https://raw.githubusercontent.com/chancesweetser/Weather-Tracker/master/assets/WeatherSearch2.png?token=AAMAN6ELCAUBIUHFW2W7CMK7WWFGW" width = "600"> <br>

### Fetching a previous search for the weather in Laramie, WY, done on November 27th

<img src = "https://raw.githubusercontent.com/chancesweetser/Weather-Tracker/master/assets/WeatherSearch3.png?token=AAMAN6CKK7VOPL3IKND3NMS7WWFG2" width = "600"> <br>

### After a failed search for the weather in 820700 (misspelled, extra '0')

<img src = "https://github.com/chancesweetser/Weather-Tracker/blob/master/assets/WeatherSearch4.PNG?token=AAMAN6CKK7VOPL3IKND3NMS7WWFG2" width = "600"> <br>
