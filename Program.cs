using System;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApp2
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			string selectedFolder = ChooseFolder();

			if (!string.IsNullOrEmpty(selectedFolder))
			{
				string[] files = Directory.GetFiles(selectedFolder);

				Console.WriteLine("Список файлов в папке:");
				foreach (string file in files)
				{
					FileInfo fileInfo = new FileInfo(file);
					Console.WriteLine($"{fileInfo.FullName}");
				}

				Console.WriteLine("\nХотите ли вы удалить файлы старше 2 лет? (Нажмите 1 для удаления, 0 для пропуска)");
				string input = Console.ReadLine();

				if (int.TryParse(input, out int choice) && (choice == 0 || choice == 1))
				{
					if (choice == 1)
					{
						DeleteOldFiles(files);
					}
					else
					{
						Console.WriteLine("Файлы не были удалены.");
					}
				}
				else
				{
					Console.WriteLine("Неверный ввод. Пожалуйста, нажмите 1 для удаления или 0 для пропуска.");
				}
			}
		}

		static string ChooseFolder()
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			if (DialogResult.OK == folderDialog.ShowDialog())
			{
				return folderDialog.SelectedPath;
			}
			return string.Empty;
		}

		static void DeleteOldFiles(string[] files)
		{
			bool filesDeleted = false;

			foreach (string file in files)
			{
				FileInfo fileInfo = new FileInfo(file);

				if (fileInfo.CreationTime > DateTime.Now.AddYears(-2))
				{
					if (File.Exists(file))
					{
						File.Delete(file);
						Console.WriteLine("Файлы удалены");
						filesDeleted = true;
					}
				}
			}

			if (filesDeleted)
			{
				Console.WriteLine("Файлы старше 2 лет были удалены.");
			}
			else
			{
				Console.WriteLine("Нет файлов для удаления старше 2 лет.");
			}
		}
	}
}
