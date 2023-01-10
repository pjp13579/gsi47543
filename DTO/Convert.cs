using AutoMapper;


namespace DTO
{
    public static class Convert<T, K>
    {
        public static K generic(T obj, K dto)
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(T), typeof(K)));
            var mapper = new Mapper(config);

            return mapper.Map<K>(obj);
        }
    }
}
