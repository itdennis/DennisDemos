using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    public class GenerateTestFileDemo
    {
        private Dictionary<String, String> rightAnswer;
        private List<string> questionList;
        private List<string> answerList;
        /// <summary>
        /// 设计思路：
        /// 1、 生成一个dictionary， 用来保存题目和答案；
        /// 2、 生成一个题目list， 保存所有题目；
        /// 3、 生成一个答案list， 保存所有的答案；
        /// 4、 设定生成的试卷数量为 n；
        /// 5、 设定每个试卷需要的题目数量为 m；
        /// 6、 生成一份试卷；
        /// 7、 从题目list中抽出一道题， 如果这道题不在此次试卷中出现过，则从题库中获取这道题的正确答案， 并且从答案list中抽出3个错误答案。
        /// 如果这道题已经在试卷中出现过， 则重新从题目list中抽， 直到该题没有在试卷中出现过；
        /// 8、 将这道题的题目和所有答案写入到试卷1中， 答案写入时候用乱序写入；
        /// 9、 将7， 8循环 m 次， 之后第一份试卷生成完毕；
        /// 10、将6，7，8 循环 n 次， 生成所有试卷；
        /// </summary>
        public void Run()
        {
            InitData();
            GenerateExamFiles();
        }

        private void GenerateExamFiles()
        {
            string fileName = "Exam_";
            for (int index = 0; index < 5; index++)
            {
                List<string> tempQuestions = new List<string>();
                using (FileStream fs = File.Create(fileName+index+".txt"))
                {
                    for (int questionIndex = 0; questionIndex < 5; questionIndex++)
                    {
                        string currentQuestion;
                        string currentAnswer;
                        List<string> answers = new List<string>();
                        //从题库中随机取出题目
                        while (true)
                        {
                            //在answer list中随机找一个题目
                            Random rm = new Random();
                            int i = rm.Next(questionList.Count);
                            //判断题目是否已经在试卷中
                            if (tempQuestions.Contains(questionList[i]))
                            {
                                //重新选题
                                continue;
                            }
                            else
                            {
                                //如果不在试卷中，将该题加入到此次试卷的题目缓存中
                                tempQuestions.Add(questionList[i]);
                                //将该题目作为当前题目
                                currentQuestion = questionList[i];
                                currentAnswer = rightAnswer[currentQuestion];
                                answers.Add(currentAnswer);
                                int wrongAnswerCount = 0;
                                while (true)
                                {
                                    //获取错误answer
                                    rm = new Random();
                                    int j = rm.Next(answerList.Count);
                                    if (currentAnswer.Equals(answerList[j]))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InitData()
        {
            rightAnswer["a的省会是哪"] = "A";
            rightAnswer["b的省会是哪"] = "B";
            rightAnswer["c的省会是哪"] = "C";
            rightAnswer["d的省会是哪"] = "D";
            rightAnswer["e的省会是哪"] = "E";
            rightAnswer["f的省会是哪"] = "F";
            rightAnswer["g的省会是哪"] = "G";
            rightAnswer["h的省会是哪"] = "H";
            rightAnswer["i的省会是哪"] = "I";
            rightAnswer["j的省会是哪"] = "J";
            rightAnswer["k的省会是哪"] = "K";
            rightAnswer["l的省会是哪"] = "L";
            rightAnswer["m的省会是哪"] = "M";
            rightAnswer["n的省会是哪"] = "N";
            rightAnswer["o的省会是哪"] = "O";
            rightAnswer["p的省会是哪"] = "P";
            rightAnswer["q的省会是哪"] = "Q";
            rightAnswer["r的省会是哪"] = "R";
            Console.WriteLine("right answer init finished.");
            string[] questions = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r" };
            string[] answers = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R" };
            questionList.AddRange(questions);
            answerList.AddRange(answers);
            Console.WriteLine("questions and answers list init finished.");
        }
    }
}
