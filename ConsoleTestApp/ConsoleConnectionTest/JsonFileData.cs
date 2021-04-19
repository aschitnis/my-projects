namespace ConsoleConnectionTest.text.json
{
    public abstract class JsonFileData
    {
        public abstract string JsonFilePath { get; set; }
        public abstract void WriteJsonToTextFile();
    }
}
