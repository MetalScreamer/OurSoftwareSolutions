using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Oss.Common.Interfaces;
using Oss.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private string userText;
        private IEnumerable<int> randomizedNums;
        private IEnumerable<int> sortedNums;
        private IEnumerable<string> stringResults;
        private int userNum = 10;
        private readonly IExpressionService expressionService;
        private string expressionValidationMessage;
        private string parsingError;
        private string selectedToken;

        public string ExpressionValidationMessage
        {
            get { return expressionValidationMessage; }
            private set { SetProperty(value, ref expressionValidationMessage); }
        }

        public string SelectedToken
        {
            get { return selectedToken; }
            set { SetProperty(value, ref selectedToken); }
        }

        public string UserText
        {
            get { return userText; }
            set
            {
                if (SetProperty(value, ref userText))
                {
                    try
                    {
                        StringResults = expressionService.GetTokens(value).Select(t => t.ToString());
                        ParsingError = null;
                        SelectedToken = StringResults.LastOrDefault();
                    }
                    catch (Exception ex)
                    {
                        ParsingError = ex.Message;
                        ExpressionValidationMessage = null;
                        StringResults = null;
                    }
                }
            }
        }

        public string ParsingError
        {
            get { return parsingError; }
            set { SetProperty(value, ref parsingError); }
        }

        public int UserNum
        {
            get { return userNum; }
            set { SetProperty(value, ref userNum); }
        }

        public IEnumerable<int> RandomizedNums
        {
            get { return randomizedNums; }
            private set { SetProperty(value, ref randomizedNums); }
        }

        public IEnumerable<int> SortedNums
        {
            get { return sortedNums; }
            private set { SetProperty(value, ref sortedNums); }
        }

        public IEnumerable<string> StringResults
        {
            get { return stringResults; }
            private set { SetProperty(value, ref stringResults); }
        }

        public NotifyTaskCompletion<string> LoadingTask { get; } = new NotifyTaskCompletion<string>(Load());

        public AsynchronousCommand DoRandomizeAndSort { get; }
        public AsynchronousCommand ExecuteExpression { get; }
        public AsynchronousCommand GetProducts { get; }

        public MainWindowViewModel(IExpressionService expressionService)
        {
            this.expressionService = expressionService;

            DoRandomizeAndSort = new AsynchronousCommand(DoSorts);
            ExecuteExpression = new AsynchronousCommand(DoExecuteExpression);
            GetProducts = new AsynchronousCommand(DoGetProducts);
        }

        private async Task DoGetProducts()
        {
            await Task.Run(() => Debug.WriteLine("Test"));
            //using (var webClient = new HttpClient())
            //{
            //    webClient.BaseAddress = new Uri("http://localhost:64539/api/");
            //    var response = await webClient.GetAsync("products");

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            //        StringResults = products.Select(p => p.ToString());
            //    }
            //}

        }

        private async Task DoExecuteExpression()
        {
            try
            {
                var rand = new Random();

                ParsingError = null;

                var initialScript = CSharpScript.Create(
                    @"
                    int X = 5;
                    int Y = 7;
                    ");

                if (rand.NextBool())
                    initialScript = initialScript.ContinueWith("int Y = 0;");

                var resultList = new List<string>();
                for (int i = 0; i < 25; i++)
                {
                    var newState = initialScript.ContinueWith($"int Z = {rand.Next(-100, 101)};");
                    var result = (await newState.ContinueWith<int>(UserText).RunAsync()).ReturnValue;
                    resultList.Add(result.ToString());
                }

                StringResults = resultList;
            }
            catch (Exception ex)
            {
                ParsingError = ex.Message;
                ExpressionValidationMessage = null;
            }
        }

        public class Globals
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private async Task DoSorts()
        {
            var rand = new Random();
            PopulateRandomizedNums(rand);

            await Task.WhenAll(
                QuickSort(rand),
                SelectionSort());
        }

        private async Task Hanoi()
        {
            StringResults = null;

            var moves = new List<string>();

            Action<int, string, string, string> hanoi = null;
            hanoi =
                (n, source, dest, aux) =>
                {
                    if (n <= 0) return;
                    hanoi(n - 1, source, aux, dest);
                    moves.Add($"Move disk from {source} to {dest}");
                    hanoi(n - 1, aux, dest, source);
                };

            await Task.Run(() => hanoi(UserNum, "Left", "Right", "Center"));

            StringResults = moves;
        }

        private async Task MergeSort()
        {
            SortedNums = null;

            var arr = RandomizedNums.ToArray();
            var aux = new int[arr.Length];

            var merge = new Action<int, int, int>(
                (start, mid, end) =>
                {
                    for (int idx = start; idx <= end; idx++)
                    {
                        aux[idx] = arr[idx];
                    }

                    var i = start;
                    var j = mid + 1;
                    var curr = start;

                    while (i <= mid && j <= end)
                    {
                        if (aux[i] <= aux[j])
                        {
                            arr[curr++] = aux[i++];
                        }
                        else
                        {
                            arr[curr++] = aux[j++];
                        }
                    }

                    while (i <= mid)
                    {
                        arr[curr++] = aux[i++];
                    }
                });

            Action<int, int> ms = null;
            ms =
                (start, end) =>
                {
                    if (start >= end) return;
                    var mid = (start + end) / 2;
                    ms(start, mid);
                    ms(mid + 1, end);
                    merge(start, mid, end);
                };

            await Task.Run(() => ms(0, arr.Length - 1));

            SortedNums = arr;
        }

        private async Task QuickSort(Random rand)
        {
            SortedNums = null;
            var arr = RandomizedNums.ToArray();

            Action<int, int> qs = null;
            qs =
                (start, end) =>
                {
                    if (start < end)
                    {
                        var pivot = rand.Next(start, end + 1);
                        Swap(ref arr[pivot], ref arr[end]);
                        pivot = end;

                        var i = start;
                        var j = end - 1;

                        while (i < j)
                        {
                            while (i < j && arr[i] <= arr[pivot])
                            {
                                i++;
                            }

                            if (i < j)
                            {
                                Swap(ref arr[i], ref arr[j--]);
                            }

                            while (i < j && arr[j] > arr[pivot])
                            {
                                j--;
                            }

                            if (i < j)
                            {
                                Swap(ref arr[j], ref arr[i++]);
                            }
                        }

                        if (arr[i] < arr[pivot]) i++;

                        Swap(ref arr[i], ref arr[pivot]);
                        qs(start, i - 1);
                        qs(i + 1, end);
                    }
                };


            await Task.Run(() => { qs(0, arr.Length - 1); });

            SortedNums = arr;
        }

        private async Task GnomeSort()
        {
            SortedNums = null;
            var arr = RandomizedNums.ToArray();

            var pos = 0;

            await Task.Run(
                () =>
                {
                    while (pos < arr.Length - 1)
                    {
                        if (arr[pos + 1] < arr[pos])
                        {
                            Swap(ref arr[pos + 1], ref arr[pos--]);
                        }
                        else
                        {
                            pos++;
                        }

                        if (pos < 0) pos = 0;
                    }
                });

            SortedNums = arr;
        }

        private async Task BubbleSort()
        {
            SortedNums = null;
            var arr = RandomizedNums.ToArray();

            await Task.Run(
                () =>
                {
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        for (int j = 0; j < arr.Length - i - 1; j++)
                        {
                            if (arr[j] > arr[j + 1])
                            {
                                Swap(ref arr[j], ref arr[j + 1]);
                            }
                        }
                    }
                });

            SortedNums = arr;
        }

        private async Task InsertionSort()
        {
            StringResults = null;
            var arr = RandomizedNums.ToArray();

            await Task.Run(
                () =>
                {
                    for (int i = 1; i < arr.Length; i++)
                    {
                        var j = i;
                        while (j > 0 && arr[j] < arr[j - 1])
                        {
                            Swap(ref arr[j], ref arr[j-- - 1]);
                        }
                    }
                });

            StringResults = arr.Select(n => n.ToString());
        }

        private async Task SelectionSort()
        {
            StringResults = null;
            var arr = RandomizedNums.ToArray();

            await Task.Run(
                () =>
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        var indexOfMinValue = i;

                        for (int j = i + 1; j < arr.Length; j++)
                        {
                            if (arr[j] < arr[indexOfMinValue])
                            {
                                indexOfMinValue = j;
                            }
                        }

                        Swap(ref arr[indexOfMinValue], ref arr[i]);
                    }
                });

            StringResults = arr.Select(n => n.ToString());
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private void PopulateRandomizedNums(Random rand)
        {
            var list = new List<int>();

            for (int _ = 0; _ < UserNum; _++)
            {
                list.Add(rand.Next());
            }

            RandomizedNums = list.AsReadOnly();
        }

        private static async Task<string> Load()
        {
            await Task.Delay(8000);
            return "Faggot";
        }
    }
}
