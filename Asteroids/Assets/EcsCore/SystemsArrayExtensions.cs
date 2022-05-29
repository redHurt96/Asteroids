namespace EcsCore
{
    public static class SystemsArrayExtensions
    {
        public static SystemsArray Add(this SystemsArray array, ISystem system)
        {
            if (system is IInitSystem initSystem)
                array.InitSystems.Add(initSystem);

            if (system is IUpdateSystem updateSystem)
                array.UpdateSystems.Add(updateSystem);

            if (system is IDisposeSystem disposeSystem)
                array.DisposeSystems.Add(disposeSystem);

            return array;
        }

        public static void Init(this SystemsArray array) => 
            array.InitSystems.ForEach(x => x.Init());

        public static void Update(this SystemsArray array) => 
            array.UpdateSystems.ForEach(x => x.Update());

        public static void Dispose(this SystemsArray array) => 
            array.DisposeSystems.ForEach(x => x.Dispose());
    }
}