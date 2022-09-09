
using Topshelf;
using WindowsServices.Topshelf;

HostFactory.Run(x =>
{
    x.Service<CustomService>();
    x.SetServiceName("TopshelfCustomService");
    x.SetDescription("TopshelfCustomService");

    x.EnableServiceRecovery(x =>
    {
        x.RestartService(TimeSpan.FromSeconds(1))
        .RestartService(TimeSpan.FromSeconds(3))
        .RestartService(TimeSpan.FromSeconds(5));


        x.SetResetPeriod(5);
    });
    
    
    x.RunAsLocalSystem();
    x.StartAutomaticallyDelayed();
});