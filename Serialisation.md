# Introduction #

Starting with [r102](https://code.google.com/p/syncbutler/source/detail?r=102), we have began refactoring Serialisation to be a part of the Partnership class and the ISyncables. (As of [r107](https://code.google.com/p/syncbutler/source/detail?r=107), these methods are now fully in use.) This documents describes how these object are serialised and recreated

# Details #

## Serialisation ##

There are two methods exposed for serialisation:

  * `string Serialize()`
  * `void SerializeXML(XmlWriter xmlData)`

`Serialize()` will return a string which is actually XML data that may be used to almost re-create the object. `SerializeXML()` accepts an XmlWriter and will use it to write the its XML data. In fact, `Serialize()` calls `SerializeXML()` to write the XML data to its XmlWriter.

## Unserialisation ##

Unserialising an object involves a two step process:
  * Isolate the XML element describing the object.
  * Call the static method `SyncEnviroment.ReflectiveUnserialize(string xmlString)`

`ReflectiveUnserialize()` will determine the class type and invoke the constructor for that object in order to instantiate the object. The object **must** have a constructor of the form `ClassConstructor(XmlReader xmlData)` or the process will fail.

Note that not the entire state of the object is restore. For example, the `parentPartnerships` of the ISyncables are not restored and must be restored manually, or by the `Partnership` constructor. While the `Partnership` constructor will restore it's ISyncables' `parentPartnership`, it will not restore it's reference to the SyncEnviroment's dictionary. This needs to be done by SyncEnviroment itself.

In addition to that, `ReflectiveUnserialize()` will only search for classes in the SyncButler namespace, or more accurately, in the SyncButler.dll assembly. It is worth noting that `ReflectiveUnserialize()` will call `InitSyncButlerAssembly()` if the assembly has not been found before. The purpose of `InitSyncButlerAssembly()` is to look for the assembly containing the SyncButler namespace.