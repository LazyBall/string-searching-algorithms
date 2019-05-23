using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    /// <summary>
    /// Представляет алгоритм поиска подстроки в строке.
    /// </summary>
    public interface IStringSearchingAlgorithm
    {
        ///  <summary>
        ///  Находит все вхождения образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Перечисление индексов вхождения образца в строку, в которой осуществляется поиск,
        ///  в порядке возрастания.
        ///  </returns>
        ///  <exception cref="System.ArgumentNullException">text или pattern null</exception>
        ///  <param name = "pattern">Строка, вхождения которой нужно найти.</param>
        ///  <param name = "text">Строка, в которой осуществляется поиск.</param>
        IEnumerable<int> GetAllEntries(string pattern, string text);

        ///  <summary>
        ///  Находит первое вхождение образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Индекс первого вхождения или -1, если вхождение не найдено.
        ///  </returns>
        ///  <exception cref="System.ArgumentNullException">text или pattern null</exception>
        ///  <param name = "pattern">Строка, вхождение которой нужно найти.</param>
        ///  <param name = "text">Строка, в которой осуществляется поиск.</param>
        int GetFirstEntry(string pattern, string text);

    }
}