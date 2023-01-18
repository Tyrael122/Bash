using CmdDirectories.Entities;
using CmdDirectories.Entities.Services;

namespace Default
{
    class Default
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    string[] command = Utils.HandleCommand();
                    switch (command[0])
                    {
                        case "cd":
                            Commands.ChangeDirectory(command);
                            break;
                        case "ls":
                            Commands.List();
                            break;
                        case "copy":
                            if (command.Length < 3) { Console.WriteLine("Usage: copy <source> <destination>"); }
                            else { Commands.Copy(command[1], command[2]); }
                            break;
                        case "move":
                            if (command.Length < 3) { Console.WriteLine("Usage: move <source> <destination>"); }
                            else { Commands.Move(command[1], command[2]); }
                            break;
                        case "rm":
                            if (command.Length < 2) { Console.WriteLine("Usage: rm <path>"); }
                            else { Commands.Remove(command[1]); }
                            break;
                        case "rename":
                            if (command.Length < 3) { Console.WriteLine("Usage: rename <source> <target_name>"); }
                            else { Commands.Rename(command[1], command[2]); }
                            break;
                        case "mkdir":
                            if (command.Length < 2) { Console.WriteLine("Usage: mkdir <directory_path>"); }
                            else { Commands.CreateDirectory(command[1]); }
                            break;
                        case "mkfile":
                            if (command.Length < 2) { Console.WriteLine("Usage: mkfile <file_path>"); }
                            else { Commands.CreateFile(command[1]); }
                            break;
                        case "ps":
                            Commands.ProcessesStatus();
                            break;
                        case "myip":
                            Commands.GetIPAddress();
                            break;
                        default:
                            break;
                    }

                }
                catch (FileNotFoundException e) { Console.WriteLine(e.Message); }
                catch (IOException e)
                {
                    Utils.ClearLine();
                    Console.WriteLine("An error has ocurred: " + e.Message);
                    continue;
                }
            }
        }
    }
}