namespace Browsely.Modules.Node.Application.Docker;

public sealed class DockerContainerConfig
{
    public string ImageName { get; set; }
    public string ContainerName { get; set; }
    public string Tag { get; set; }
    public int HostPort { get; set; }
    public int ContainerPort { get; set; }
}
