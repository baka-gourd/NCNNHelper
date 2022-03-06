using System.Diagnostics;
using System.Text.Json;
using System.Threading.Channels;

using NCNNHelper;

if (File.Exists("config.json"))
{
    var conf = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"), ConfigContext.Default.Config);
    if (conf is null)
    {
        Console.WriteLine("Error config.");
        goto end;
    }

    if (string.IsNullOrEmpty(conf.ProcessName))
    {
        Console.WriteLine("Process name is invalid.");
        goto end;
    }

    var dict = new Dictionary<string, string>();

    if (conf.Args is not null)
    {
        foreach (var arg in conf.Args)
        {
            Console.Write(arg.Description);
            var value = Console.ReadLine();
            if (!(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)))
            {
                dict.Add(arg.Option!, value);
            }
            else
            {
                dict.Add(arg.Option!, arg.Default!);
            }
        }
    }

    var argument = string.Concat(dict.Select(arg => $"{arg.Key} {arg.Value} "));
    var process = Process.Start(conf.ProcessName, argument);
    process.OutputDataReceived += (_, e) =>
    {
        if (e.Data is null)
        {
            return;
        }

        Console.WriteLine(e.Data);
    };
    process.Exited += (_, _) => { Console.WriteLine("Success!"); };
}
else
{
    Console.WriteLine("Missing config!");
}

end:
Console.ReadLine();