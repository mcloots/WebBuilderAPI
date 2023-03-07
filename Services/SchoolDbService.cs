using WebBuilder.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebBuilder.Helpers;

namespace WebBuilder.Services;

public class SchoolDbService
{
    private readonly IMongoCollection<School> _schoolCollection;

    public SchoolDbService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _schoolCollection = database.GetCollection<School>(Collections.SCHOOL);
    }

    public async Task CreateAsync(School school)
    {
        await _schoolCollection.InsertOneAsync(school);
        return;
    }

    public async Task<List<School>> GetAsync()
    {
        //No filter = empty BsonDocument
        return await _schoolCollection.Find(new BsonDocument()).ToListAsync();
    }

    //public async Task AddToPlaylistAsync(string id, string movieId)
    //{
    //    FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
    //    UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("items", movieId);
    //    await _playlistCollection.UpdateOneAsync(filter,update);
    //    return;
    //}

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<School> filter = Builders<School>.Filter.Eq("Id", id);
        await _schoolCollection.DeleteOneAsync(filter);
        return;
    }
}

