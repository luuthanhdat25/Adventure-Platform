using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository <T>
{
    protected readonly string path;
    public Repository(string path) => this.path = path;

    // Loads data of type T from the file specified by the path.
    protected virtual T Get() => JsonService.LoadData<T>(path);

    // Saves the provided data of type T to the file specified by the path.
    protected virtual void Save(T data) => JsonService.SaveData(path, data);
}
