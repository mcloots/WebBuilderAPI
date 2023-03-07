using WebBuilder.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebBuilder.Helpers;

namespace WebBuilder.Services;

public class GroupDbService
{
    private readonly IMongoCollection<Group> _groupCollection;
    private readonly IMongoCollection<School> _schoolCollection;

    public GroupDbService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _groupCollection = database.GetCollection<Group>(Collections.GROUP);
        _schoolCollection = database.GetCollection<School>(Collections.SCHOOL);
    }

    public async Task CreateAsync(Group group)
    {
        await _groupCollection.InsertOneAsync(group);

        //add this group to the school
        FilterDefinition<School> filter = Builders<School>.Filter.Eq("Id", group.schoolId);
        UpdateDefinition<School> update = Builders<School>.Update.AddToSet<string>("items", group.Id);
        await _schoolCollection.UpdateOneAsync(filter, update);

        return;
    }

    public async Task<List<Group>> GetAsync()
    {
        //No filter = empty BsonDocument
        return await _groupCollection.Find(new BsonDocument()).ToListAsync();
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
        FilterDefinition<Group> filter = Builders<Group>.Filter.Eq("Id", id);
        await _groupCollection.DeleteOneAsync(filter);
        return;
    }
}

