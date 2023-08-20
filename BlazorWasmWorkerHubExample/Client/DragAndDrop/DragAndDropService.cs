namespace BlazorWasmWorkerHubExample.Client.DragAndDrop
{
    public class DragAndDropService
    {
        public object? Data { get; set; }
        public string? Zone { get; set; }

        public void StartDrag(object? data, string? zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        //TODO: Generell nicht schlecht, Die Accept methode ist unnötig. Beim Drop muss mehr info mitgegeben werden. 

        public bool Accepts(string? zone)
        {
            return this.Zone == zone;
        }
    }
}
