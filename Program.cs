using CmdDirectories.Entities;

namespace Default
{
    class Default
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] command = Helpers.HandleCommand();
                switch (command[0])
                {
                    case "cd":
                        Cmd.ChangeDirectory(command);
                        break;
                    case "ls":
                        Cmd.List();
                        break;
                    case "copy":
                        if (command.Length < 3) { Console.WriteLine("Usage: copy <source> <destination>"); }
                        else { Cmd.Copy(command[1], command[2]); }
                        break;
                    case "move":
                        if (command.Length < 3) { Console.WriteLine("Usage: move <source> <destination>"); }
                        else { Cmd.Move(command[1], command[2]); }
                        break;
                    case "rm":
                        if (command.Length < 2) { Console.WriteLine("Usage: rm <path>"); }
                        else { Cmd.Remove(command[1]); }
                        break;
                    case "rename":
                        if (command.Length < 3) { Console.WriteLine("Usage: rename <source> <target_name>"); }
                        else { Cmd.Rename(command[1], command[2]); }
                        break;
                    case "mkdir":
                        if (command.Length < 2) { Console.WriteLine("Usage: mkdir <directory_path>"); }
                        else { Cmd.CreateDirectory(command[1]); }
                        break;
                    case "mkfile":
                        if (command.Length < 2) { Console.WriteLine("Usage: mkfile <file_path>"); }
                        else { Cmd.CreateFile(command[1]); }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}