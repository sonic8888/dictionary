﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDictionary.EF;
using XMLRead;

namespace MyDictionary.Tools
{
    public static class BdTools
    {
        public static int AddNewWord(string word, string soundname = "", string partofspeach = "", string transcription = "")
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var words = new MyWord() { Word = word, SoundName = soundname, PartOfSpeach = partofspeach, Transcription = transcription };
                    context.MyWords.Add(words);
                    context.SaveChanges();
                    return words.WordId;
                }
                catch (Exception)
                {

                    return 0;
                }

            }

        }
        public static int AddNewTranslate(string str, int wordid)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var tr = new MyTranslate() { Translate = str, WordId = wordid };
                    context.MyTranslates.Add(tr);
                    context.SaveChanges();
                    return tr.TranslateId;
                }
                catch (Exception)
                {

                    return 0;
                }

            }
        }
        public static int AddNewExamle(string str, int wordid)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    var tr = new MyExample() { Example = str, WordId = wordid };
                    context.MyExamples.Add(tr);
                    context.SaveChanges();
                    return tr.ExampleId;
                }
                catch (Exception)
                {

                    return 0;
                }

            }
        }
    

        public static int AddNewWords(WordSample wordSample)
        {
            int index = AddNewWord(wordSample.Word, wordSample.SoundName, wordSample.PartOfSpeach, wordSample.Transcription);
            if (index > 0)
            {
                foreach (string trans in wordSample.Translate)
                {
                    AddNewTranslate(trans, index);
                }


                foreach (string item in wordSample.Example)
                {
                    AddNewExamle(item, index);
                }

            }
            return index;
        }
        public static MyWord DeleteWord(int id)
        {
            using (var context = new ApplicationContext())
            {
                try
                {
                    MyWord mw = context.MyWords.Find(id);
                    if (mw != null)
                    {
                        context.MyWords.Remove(mw);
                        context.SaveChanges();
                    }
                    return mw;
                }
                catch (Exception)
                {
                    return null;


                }

            }
        }
    }
}
