// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: domain/user_friendship_status.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from domain/user_friendship_status.proto</summary>
public static partial class UserFriendshipStatusReflection {

  #region Descriptor
  /// <summary>File descriptor for domain/user_friendship_status.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static UserFriendshipStatusReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CiNkb21haW4vdXNlcl9mcmllbmRzaGlwX3N0YXR1cy5wcm90bypfChRVc2Vy",
          "RnJpZW5kc2hpcFN0YXR1cxIWChJTVEFUVVNfVU5TUEVDSUZJRUQQABILCgdQ",
          "RU5ESU5HEAESCQoFQkxPQ0sQAhIKCgZBQ0NFUFQQAxILCgdERUNMSU5FEAQq",
          "VAoUVXNlckFjdGlvbkZyaWVuZHNoaXASFgoSQUNUSU9OX1VOU1BFQ0lGSUVE",
          "EAASEAoMQUNUSU9OX0JMT0NLEAESEgoOQUNUSU9OX1VOQkxPQ0sQAkKQAQom",
          "aW8ubXV6aXZlcnNlLnByb3RvLnVzZXJhY3Rpdml0eS5kb21haW5CE1VzZXJG",
          "cmllbmRzaGlwUHJvdG9QAVpPZ2l0bGFiLmNvbS9tdXppdmVyc2UvY29tbW9u",
          "L3Byb3RvZ29nZW5lcmF0b3IvdXNlci1hY3Rpdml0eS1wcm90by1hcGkvcGtn",
          "L2RvbWFpbmIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::UserFriendshipStatus), typeof(global::UserActionFriendship), }, null, null));
  }
  #endregion

}
#region Enums
public enum UserFriendshipStatus {
  [pbr::OriginalName("STATUS_UNSPECIFIED")] StatusUnspecified = 0,
  [pbr::OriginalName("PENDING")] Pending = 1,
  [pbr::OriginalName("BLOCK")] Block = 2,
  [pbr::OriginalName("ACCEPT")] Accept = 3,
  [pbr::OriginalName("DECLINE")] Decline = 4,
}

public enum UserActionFriendship {
  [pbr::OriginalName("ACTION_UNSPECIFIED")] ActionUnspecified = 0,
  [pbr::OriginalName("ACTION_BLOCK")] ActionBlock = 1,
  [pbr::OriginalName("ACTION_UNBLOCK")] ActionUnblock = 2,
}

#endregion


#endregion Designer generated code
