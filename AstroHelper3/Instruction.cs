using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;

namespace AstroHelper3;

public class Instruction
{
    private string _outputFilePath;

        public Instruction(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
        }

        public void WriteInstruction()
        {
            try
            {
                string description = "В каждом параграфе пункты со звездочками - это набор вариантов при таком положении планеты\nТо есть может совпадать, например, только 1 пункт из параграфа и он компенсирует все остальные\nЕсли какой-то пункт совсем не совпадает - это потому что вы успешно его компенсируете каким-то другим аспектом из натальной карты\nСледовательно, если совпадает какой-то негативный пункт (например, связанные со здоровьем), то его можно компенсировать обретением другого пункта (например работа, увлечения и тд), тогда негативному аспекту не останется места.\nТак работает астрология )\nТак что не пугайтесь, если нашли что-то плохое - это может и не проиграться, так как сильнее влияет какой-то другой аспект\n\nОписание идет от 3-ого лица (Он = человек с таким аспектом)\n\nВ конце идет описание Кармических узлов. Кету - это набор навыков, с которыми мы рождаемся. Раху - те черты, к которым нам надо стремиться в течение жизни\n\n\n";
        
                using (var writer = new StreamWriter(_outputFilePath, false))  // true для дополнения файла, false для перезаписи
                {
                    writer.WriteLine(description);
                }
                Console.WriteLine("Описание было записано в файл: " + _outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи в файл: " + ex.Message);
            }
        }
    }

