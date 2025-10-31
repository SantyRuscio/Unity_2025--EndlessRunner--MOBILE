using System.Collections.Generic;

public static class LenguageU
{
    public static Dictionary<Lang, Dictionary<string, string>> LoadTranslate(DatLocalization[] data)
    {
        var tempDic = new Dictionary<Lang, Dictionary<string, string>>();

        for(int i = 0; i < data.Length; i++)
        {
            var tempData = new Dictionary<string, string>();

            foreach(var item in data[i].data)
            {
                var f = item.text.Split(',');
                
                foreach(var item2 in f)
                {
                    var c = item2.Replace('"',' ')
                                 .Split(':');

                    if(c.Length == 2)
                        tempData.Add(c[0].Trim(), c[1].Trim());
                }
            }

            tempDic.Add(data[i].language, tempData);
        }
        return tempDic;
    }
}
