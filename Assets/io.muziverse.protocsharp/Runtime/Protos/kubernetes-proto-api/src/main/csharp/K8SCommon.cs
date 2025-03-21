// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: domain/k8s_common.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from domain/k8s_common.proto</summary>
public static partial class K8SCommonReflection {

  #region Descriptor
  /// <summary>File descriptor for domain/k8s_common.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static K8SCommonReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Chdkb21haW4vazhzX2NvbW1vbi5wcm90byKcAQoKR2FtZVNlcnZlchIWCg5n",
          "YW1lX3NlcnZlcl9pZBgBIAEoCRIPCgdhZGRyZXNzGAIgASgJEhEKCW5vZGVf",
          "bmFtZRgDIAEoCRINCgVzdGF0ZRgEIAEoCRIfCgVwb3J0cxgFIAMoCzIQLkdh",
          "bWVTZXJ2ZXIuUG9ydBoiCgRQb3J0EgwKBG5hbWUYASABKAkSDAoEcG9ydBgC",
          "IAEoBSK0AQoRR2FtZUZsZWV0UmVzcG9uc2USEAoIZmxlZXRfaWQYASABKAkS",
          "EQoJbmFtZXNwYWNlGAIgASgJEiIKCmZsZWV0X3R5cGUYAyABKA4yDi5HYW1l",
          "RmxlZXRUeXBlEhMKC2dhbWVfc3ltYm9sGAQgASgJEh4KFm51bWJlcl9vZl9n",
          "YW1lX3NlcnZlcnMYBSABKAMSIQoMZ2FtZV9zZXJ2ZXJzGAYgAygLMgsuR2Ft",
          "ZVNlcnZlcipaCg1HYW1lRmxlZXRUeXBlEhoKFkZMRUVUX1RZUEVfVU5TUEVD",
          "SUZJRUQQABITCg9JU0xBTkRfSU5TVEFOQ0UQARIYChRHQU1FX1NFUlZFUl9J",
          "TlNUQU5DRRACQoMBCiRpby5tdXppdmVyc2UucHJvdG8ua3ViZXJuZXRlcy5k",
          "b21haW5CC0NvbW1vblByb3RvUAFaTGdpdGxhYi5jb20vbXV6aXZlcnNlL2Nv",
          "bW1vbi9wcm90b2dvZ2VuZXJhdG9yL2t1YmVybmV0ZXMtcHJvdG8tYXBpL3Br",
          "Zy9kb21haW5iBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::GameFleetType), }, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::GameServer), global::GameServer.Parser, new[]{ "GameServerId", "Address", "NodeName", "State", "Ports" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::GameServer.Types.Port), global::GameServer.Types.Port.Parser, new[]{ "Name", "Port_" }, null, null, null, null)}),
          new pbr::GeneratedClrTypeInfo(typeof(global::GameFleetResponse), global::GameFleetResponse.Parser, new[]{ "FleetId", "Namespace", "FleetType", "GameSymbol", "NumberOfGameServers", "GameServers" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Enums
public enum GameFleetType {
  [pbr::OriginalName("FLEET_TYPE_UNSPECIFIED")] FleetTypeUnspecified = 0,
  [pbr::OriginalName("ISLAND_INSTANCE")] IslandInstance = 1,
  [pbr::OriginalName("GAME_SERVER_INSTANCE")] GameServerInstance = 2,
}

#endregion

#region Messages
public sealed partial class GameServer : pb::IMessage<GameServer>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<GameServer> _parser = new pb::MessageParser<GameServer>(() => new GameServer());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<GameServer> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::K8SCommonReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameServer() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameServer(GameServer other) : this() {
    gameServerId_ = other.gameServerId_;
    address_ = other.address_;
    nodeName_ = other.nodeName_;
    state_ = other.state_;
    ports_ = other.ports_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameServer Clone() {
    return new GameServer(this);
  }

  /// <summary>Field number for the "game_server_id" field.</summary>
  public const int GameServerIdFieldNumber = 1;
  private string gameServerId_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string GameServerId {
    get { return gameServerId_; }
    set {
      gameServerId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "address" field.</summary>
  public const int AddressFieldNumber = 2;
  private string address_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Address {
    get { return address_; }
    set {
      address_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "node_name" field.</summary>
  public const int NodeNameFieldNumber = 3;
  private string nodeName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string NodeName {
    get { return nodeName_; }
    set {
      nodeName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "state" field.</summary>
  public const int StateFieldNumber = 4;
  private string state_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string State {
    get { return state_; }
    set {
      state_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "ports" field.</summary>
  public const int PortsFieldNumber = 5;
  private static readonly pb::FieldCodec<global::GameServer.Types.Port> _repeated_ports_codec
      = pb::FieldCodec.ForMessage(42, global::GameServer.Types.Port.Parser);
  private readonly pbc::RepeatedField<global::GameServer.Types.Port> ports_ = new pbc::RepeatedField<global::GameServer.Types.Port>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::GameServer.Types.Port> Ports {
    get { return ports_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as GameServer);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(GameServer other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (GameServerId != other.GameServerId) return false;
    if (Address != other.Address) return false;
    if (NodeName != other.NodeName) return false;
    if (State != other.State) return false;
    if(!ports_.Equals(other.ports_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (GameServerId.Length != 0) hash ^= GameServerId.GetHashCode();
    if (Address.Length != 0) hash ^= Address.GetHashCode();
    if (NodeName.Length != 0) hash ^= NodeName.GetHashCode();
    if (State.Length != 0) hash ^= State.GetHashCode();
    hash ^= ports_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (GameServerId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(GameServerId);
    }
    if (Address.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Address);
    }
    if (NodeName.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(NodeName);
    }
    if (State.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(State);
    }
    ports_.WriteTo(output, _repeated_ports_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (GameServerId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(GameServerId);
    }
    if (Address.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Address);
    }
    if (NodeName.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(NodeName);
    }
    if (State.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(State);
    }
    ports_.WriteTo(ref output, _repeated_ports_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (GameServerId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(GameServerId);
    }
    if (Address.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Address);
    }
    if (NodeName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(NodeName);
    }
    if (State.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(State);
    }
    size += ports_.CalculateSize(_repeated_ports_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(GameServer other) {
    if (other == null) {
      return;
    }
    if (other.GameServerId.Length != 0) {
      GameServerId = other.GameServerId;
    }
    if (other.Address.Length != 0) {
      Address = other.Address;
    }
    if (other.NodeName.Length != 0) {
      NodeName = other.NodeName;
    }
    if (other.State.Length != 0) {
      State = other.State;
    }
    ports_.Add(other.ports_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          GameServerId = input.ReadString();
          break;
        }
        case 18: {
          Address = input.ReadString();
          break;
        }
        case 26: {
          NodeName = input.ReadString();
          break;
        }
        case 34: {
          State = input.ReadString();
          break;
        }
        case 42: {
          ports_.AddEntriesFrom(input, _repeated_ports_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          GameServerId = input.ReadString();
          break;
        }
        case 18: {
          Address = input.ReadString();
          break;
        }
        case 26: {
          NodeName = input.ReadString();
          break;
        }
        case 34: {
          State = input.ReadString();
          break;
        }
        case 42: {
          ports_.AddEntriesFrom(ref input, _repeated_ports_codec);
          break;
        }
      }
    }
  }
  #endif

  #region Nested types
  /// <summary>Container for nested types declared in the GameServer message type.</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static partial class Types {
    public sealed partial class Port : pb::IMessage<Port>
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        , pb::IBufferMessage
    #endif
    {
      private static readonly pb::MessageParser<Port> _parser = new pb::MessageParser<Port>(() => new Port());
      private pb::UnknownFieldSet _unknownFields;
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public static pb::MessageParser<Port> Parser { get { return _parser; } }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public static pbr::MessageDescriptor Descriptor {
        get { return global::GameServer.Descriptor.NestedTypes[0]; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      pbr::MessageDescriptor pb::IMessage.Descriptor {
        get { return Descriptor; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public Port() {
        OnConstruction();
      }

      partial void OnConstruction();

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public Port(Port other) : this() {
        name_ = other.name_;
        port_ = other.port_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public Port Clone() {
        return new Port(this);
      }

      /// <summary>Field number for the "name" field.</summary>
      public const int NameFieldNumber = 1;
      private string name_ = "";
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public string Name {
        get { return name_; }
        set {
          name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        }
      }

      /// <summary>Field number for the "port" field.</summary>
      public const int Port_FieldNumber = 2;
      private int port_;
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public int Port_ {
        get { return port_; }
        set {
          port_ = value;
        }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public override bool Equals(object other) {
        return Equals(other as Port);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public bool Equals(Port other) {
        if (ReferenceEquals(other, null)) {
          return false;
        }
        if (ReferenceEquals(other, this)) {
          return true;
        }
        if (Name != other.Name) return false;
        if (Port_ != other.Port_) return false;
        return Equals(_unknownFields, other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public override int GetHashCode() {
        int hash = 1;
        if (Name.Length != 0) hash ^= Name.GetHashCode();
        if (Port_ != 0) hash ^= Port_.GetHashCode();
        if (_unknownFields != null) {
          hash ^= _unknownFields.GetHashCode();
        }
        return hash;
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public override string ToString() {
        return pb::JsonFormatter.ToDiagnosticString(this);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public void WriteTo(pb::CodedOutputStream output) {
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        output.WriteRawMessage(this);
      #else
        if (Name.Length != 0) {
          output.WriteRawTag(10);
          output.WriteString(Name);
        }
        if (Port_ != 0) {
          output.WriteRawTag(16);
          output.WriteInt32(Port_);
        }
        if (_unknownFields != null) {
          _unknownFields.WriteTo(output);
        }
      #endif
      }

      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
        if (Name.Length != 0) {
          output.WriteRawTag(10);
          output.WriteString(Name);
        }
        if (Port_ != 0) {
          output.WriteRawTag(16);
          output.WriteInt32(Port_);
        }
        if (_unknownFields != null) {
          _unknownFields.WriteTo(ref output);
        }
      }
      #endif

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public int CalculateSize() {
        int size = 0;
        if (Name.Length != 0) {
          size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
        }
        if (Port_ != 0) {
          size += 1 + pb::CodedOutputStream.ComputeInt32Size(Port_);
        }
        if (_unknownFields != null) {
          size += _unknownFields.CalculateSize();
        }
        return size;
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public void MergeFrom(Port other) {
        if (other == null) {
          return;
        }
        if (other.Name.Length != 0) {
          Name = other.Name;
        }
        if (other.Port_ != 0) {
          Port_ = other.Port_;
        }
        _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      public void MergeFrom(pb::CodedInputStream input) {
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        input.ReadRawMessage(this);
      #else
        uint tag;
        while ((tag = input.ReadTag()) != 0) {
          switch(tag) {
            default:
              _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
              break;
            case 10: {
              Name = input.ReadString();
              break;
            }
            case 16: {
              Port_ = input.ReadInt32();
              break;
            }
          }
        }
      #endif
      }

      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
      void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
        uint tag;
        while ((tag = input.ReadTag()) != 0) {
          switch(tag) {
            default:
              _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
              break;
            case 10: {
              Name = input.ReadString();
              break;
            }
            case 16: {
              Port_ = input.ReadInt32();
              break;
            }
          }
        }
      }
      #endif

    }

  }
  #endregion

}

public sealed partial class GameFleetResponse : pb::IMessage<GameFleetResponse>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<GameFleetResponse> _parser = new pb::MessageParser<GameFleetResponse>(() => new GameFleetResponse());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<GameFleetResponse> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::K8SCommonReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameFleetResponse() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameFleetResponse(GameFleetResponse other) : this() {
    fleetId_ = other.fleetId_;
    namespace_ = other.namespace_;
    fleetType_ = other.fleetType_;
    gameSymbol_ = other.gameSymbol_;
    numberOfGameServers_ = other.numberOfGameServers_;
    gameServers_ = other.gameServers_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GameFleetResponse Clone() {
    return new GameFleetResponse(this);
  }

  /// <summary>Field number for the "fleet_id" field.</summary>
  public const int FleetIdFieldNumber = 1;
  private string fleetId_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string FleetId {
    get { return fleetId_; }
    set {
      fleetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "namespace" field.</summary>
  public const int NamespaceFieldNumber = 2;
  private string namespace_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Namespace {
    get { return namespace_; }
    set {
      namespace_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "fleet_type" field.</summary>
  public const int FleetTypeFieldNumber = 3;
  private global::GameFleetType fleetType_ = global::GameFleetType.FleetTypeUnspecified;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::GameFleetType FleetType {
    get { return fleetType_; }
    set {
      fleetType_ = value;
    }
  }

  /// <summary>Field number for the "game_symbol" field.</summary>
  public const int GameSymbolFieldNumber = 4;
  private string gameSymbol_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string GameSymbol {
    get { return gameSymbol_; }
    set {
      gameSymbol_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "number_of_game_servers" field.</summary>
  public const int NumberOfGameServersFieldNumber = 5;
  private long numberOfGameServers_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public long NumberOfGameServers {
    get { return numberOfGameServers_; }
    set {
      numberOfGameServers_ = value;
    }
  }

  /// <summary>Field number for the "game_servers" field.</summary>
  public const int GameServersFieldNumber = 6;
  private static readonly pb::FieldCodec<global::GameServer> _repeated_gameServers_codec
      = pb::FieldCodec.ForMessage(50, global::GameServer.Parser);
  private readonly pbc::RepeatedField<global::GameServer> gameServers_ = new pbc::RepeatedField<global::GameServer>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::GameServer> GameServers {
    get { return gameServers_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as GameFleetResponse);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(GameFleetResponse other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (FleetId != other.FleetId) return false;
    if (Namespace != other.Namespace) return false;
    if (FleetType != other.FleetType) return false;
    if (GameSymbol != other.GameSymbol) return false;
    if (NumberOfGameServers != other.NumberOfGameServers) return false;
    if(!gameServers_.Equals(other.gameServers_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (FleetId.Length != 0) hash ^= FleetId.GetHashCode();
    if (Namespace.Length != 0) hash ^= Namespace.GetHashCode();
    if (FleetType != global::GameFleetType.FleetTypeUnspecified) hash ^= FleetType.GetHashCode();
    if (GameSymbol.Length != 0) hash ^= GameSymbol.GetHashCode();
    if (NumberOfGameServers != 0L) hash ^= NumberOfGameServers.GetHashCode();
    hash ^= gameServers_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (FleetId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(FleetId);
    }
    if (Namespace.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Namespace);
    }
    if (FleetType != global::GameFleetType.FleetTypeUnspecified) {
      output.WriteRawTag(24);
      output.WriteEnum((int) FleetType);
    }
    if (GameSymbol.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(GameSymbol);
    }
    if (NumberOfGameServers != 0L) {
      output.WriteRawTag(40);
      output.WriteInt64(NumberOfGameServers);
    }
    gameServers_.WriteTo(output, _repeated_gameServers_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (FleetId.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(FleetId);
    }
    if (Namespace.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Namespace);
    }
    if (FleetType != global::GameFleetType.FleetTypeUnspecified) {
      output.WriteRawTag(24);
      output.WriteEnum((int) FleetType);
    }
    if (GameSymbol.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(GameSymbol);
    }
    if (NumberOfGameServers != 0L) {
      output.WriteRawTag(40);
      output.WriteInt64(NumberOfGameServers);
    }
    gameServers_.WriteTo(ref output, _repeated_gameServers_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (FleetId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FleetId);
    }
    if (Namespace.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Namespace);
    }
    if (FleetType != global::GameFleetType.FleetTypeUnspecified) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) FleetType);
    }
    if (GameSymbol.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(GameSymbol);
    }
    if (NumberOfGameServers != 0L) {
      size += 1 + pb::CodedOutputStream.ComputeInt64Size(NumberOfGameServers);
    }
    size += gameServers_.CalculateSize(_repeated_gameServers_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(GameFleetResponse other) {
    if (other == null) {
      return;
    }
    if (other.FleetId.Length != 0) {
      FleetId = other.FleetId;
    }
    if (other.Namespace.Length != 0) {
      Namespace = other.Namespace;
    }
    if (other.FleetType != global::GameFleetType.FleetTypeUnspecified) {
      FleetType = other.FleetType;
    }
    if (other.GameSymbol.Length != 0) {
      GameSymbol = other.GameSymbol;
    }
    if (other.NumberOfGameServers != 0L) {
      NumberOfGameServers = other.NumberOfGameServers;
    }
    gameServers_.Add(other.gameServers_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          FleetId = input.ReadString();
          break;
        }
        case 18: {
          Namespace = input.ReadString();
          break;
        }
        case 24: {
          FleetType = (global::GameFleetType) input.ReadEnum();
          break;
        }
        case 34: {
          GameSymbol = input.ReadString();
          break;
        }
        case 40: {
          NumberOfGameServers = input.ReadInt64();
          break;
        }
        case 50: {
          gameServers_.AddEntriesFrom(input, _repeated_gameServers_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          FleetId = input.ReadString();
          break;
        }
        case 18: {
          Namespace = input.ReadString();
          break;
        }
        case 24: {
          FleetType = (global::GameFleetType) input.ReadEnum();
          break;
        }
        case 34: {
          GameSymbol = input.ReadString();
          break;
        }
        case 40: {
          NumberOfGameServers = input.ReadInt64();
          break;
        }
        case 50: {
          gameServers_.AddEntriesFrom(ref input, _repeated_gameServers_codec);
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
