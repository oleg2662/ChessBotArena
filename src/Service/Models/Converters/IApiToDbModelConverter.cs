//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BoardGame.Service.Models.Converters
//{
//    /// <summary>
//    /// Base interface to the API -> DB model converters.
//    /// </summary>
//    /// <typeparam name="TApiSideModel">API side model</typeparam>
//    /// <typeparam name="TDbSideModel">DB side model</typeparam>
//    public interface IApiToDbModelConverter<TApiSideModel, TDbSideModel>
//    {
//        /// <summary>
//        /// Converts the API side object to the DB target type.
//        /// </summary>
//        /// <param name="source">Source object.</param>
//        /// <returns>Null if the source is null. Otherwise the converted version.</returns>
//        TDbSideModel Convert(TApiSideModel source);
//    }
//}
