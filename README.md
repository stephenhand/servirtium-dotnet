# Servirtium .NET

.NET Core 3.1 port of the Servirtium Java implementation: https://github.com/servirtium/servirtium-java

## Examples

Recording:

```csharp
            var recorder = new MarkdownRecorder(
                ClimateApi.DEFAULT_SITE, $@"test_recordings\{script}",
                new FindAndReplaceScriptWriter(new[] {
                    new RegexReplacement(new Regex("User-Agent: .*"), "User-Agent: Servirtium-Testing")
                }, new MarkdownScriptWriter()));

            var server = AspNetCoreServirtiumServer.WithTransforms(
                1234,
                recorder,
                new SimpleInteractionTransforms(
                    ClimateApi.DEFAULT_SITE,
                    new Regex[0],
                    new[] {
                    "Date:", "X-", "Strict-Transport-Security",
                    "Content-Security-Policy", "Cache-Control", "Secure", "HttpOnly",
                    "Set-Cookie: climatedata.cookie=" }.Select(pattern => new Regex(pattern))
                ));

            server.start();
            //Some tests to record interactions using Servirtium on host 'localhost:1234'
            server.stop();
```

Replaying:

```csharp
            var replayer = new MarkdownReplayer();
            replayer.LoadScriptFile($@"test_recordings\{script}");

            AspNetCoreServirtiumServer.WithTransforms(
                1234,
                replayer,
                new SimpleInteractionTransforms(
                    ClimateApi.DEFAULT_SITE,
                    new Regex[0],
                    new[] { new Regex("Date:") }
                )),

            server.start();
            //Some tests to use the recorded interactions hosted on 'localhost:1234'
            server.stop();
```

See full demo project for more complete example code: https://github.com/stephenhand/servirtium-demo-dotnet-climate-tck

## Current Status

Currently working through the new implementation guide:

https://github.com/servirtium/README/blob/master/starting-a-new-implementation.md

All steps up until 4 are completed.

## Roadmap

Current roadmap in priority order:

* Complete steps 5-7 of the implementation guide.

* Add injectable logging support using the `Microsoft.Extensions.Logging`.

* Productionise standalone server host executable (currently used to sanity check HTTP requests from tests against those sent from postman) to offer a subset of Servirtium functionality out of process.
