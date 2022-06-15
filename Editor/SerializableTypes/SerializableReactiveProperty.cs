namespace UnityMVVM.Editor
{
    [System.Serializable]
    public class SerializableReactiveProperty
    {
        [ReadOnly]
        public string name;
        [ReadOnly]
        public string type;
        [ReadOnly]
        public string value;

        public SerializableReactiveProperty(string name, string type) : this(name, type, "None") { }
        public SerializableReactiveProperty(string name, string type, string value) => (this.name, this.type, this.value) = (name, type, value);
    }
}
