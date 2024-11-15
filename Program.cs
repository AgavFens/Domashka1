using System;
using System.IO;

namespace ConsoleApp2
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Введите полный путь к папке:");
			string selectedFolder = Console.ReadLine();

			if (string.IsNullOrEmpty(selectedFolder) || !Directory.Exists(selectedFolder))
			{
				Console.WriteLine("Папка не найдена. Проверьте путь и попробуйте снова.");
				return;
			}

			string[] files = Directory.GetFiles(selectedFolder);
			Console.WriteLine("Список файлов в папке:");
			foreach (string file in files)
			{
				FileInfo fileInfo = new FileInfo(file);
				Console.WriteLine(fileInfo.FullName);
			}

			Console.WriteLine("\nХотите ли вы удалить файлы старше 2 лет? (Нажмите 1 для удаления, 0 для пропуска)");
			string input = Console.ReadLine();

			if (!int.TryParse(input, out int choice) || (choice != 0 && choice != 1))
			{
				Console.WriteLine("Неверный ввод. Пожалуйста, нажмите 1 для удаления или 0 для пропуска.");
				return;
			}

			if (choice == 1)
				DeleteOldFiles(files);
			else
				Console.WriteLine("Файлы не были удалены.");
		}

		static void DeleteOldFiles(string[] files)
		{
			bool filesDeleted = false;

			foreach (string file in files)
			{
				FileInfo fileInfo = new FileInfo(file);

				if (fileInfo.CreationTime < DateTime.Now.AddYears(-2))
					continue;

				if (File.Exists(file))
				{
					File.Delete(file);
					Console.WriteLine($"Файл {fileInfo.FullName} удален.");
					filesDeleted = true;
				}
			}

			if (!filesDeleted)
				Console.WriteLine("Нет файлов для удаления старше 2 лет.");
			else
				Console.WriteLine("Удаление завершено.");
		}
	}
}
