// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: domain/metadata_common.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from domain/metadata_common.proto</summary>
public static partial class MetadataCommonReflection {

  #region Descriptor
  /// <summary>File descriptor for domain/metadata_common.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static MetadataCommonReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Chxkb21haW4vbWV0YWRhdGFfY29tbW9uLnByb3RvGiZkb21haW4vcmVzb3Vy",
          "Y2VfcGxhdGZvcm1fc3VwcG9ydC5wcm90byJiCgxNZXRhZGF0YUluZm8SIgoI",
          "cGxhdGZvcm0YASABKA4yEC5QbGF0Zm9ybVN1cHBvcnQSDwoHdmVyc2lvbhgC",
          "IAEoBRIMCgRuYW1lGAMgASgJEg8KB3F1YWxpdHkYBCABKAkiGgoKQmluYXJ5",
          "RmlsZRIMCgRkYXRhGAEgASgMQoUBCiJpby5tdXppdmVyc2UucHJvdG8ucmVz",
          "b3VyY2UuZG9tYWluQhFNZXRhZGF0YUluZm9Qcm90b1ABWkpnaXRsYWIuY29t",
          "L211eml2ZXJzZS9jb21tb24vcHJvdG9nb2dlbmVyYXRvci9yZXNvdXJjZS1w",
          "cm90by1hcGkvcGtnL2RvbWFpbmIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::ResourcePlatformSupportReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::MetadataInfo), global::MetadataInfo.Parser, new[]{ "Platform", "Version", "Name", "Quality" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::BinaryFile), global::BinaryFile.Parser, new[]{ "Data" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class MetadataInfo : pb::IMessage<MetadataInfo>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<MetadataInfo> _parser = new pb::MessageParser<MetadataInfo>(() => new MetadataInfo());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<MetadataInfo> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::MetadataCommonReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MetadataInfo() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MetadataInfo(MetadataInfo other) : this() {
    platform_ = other.platform_;
    version_ = other.version_;
    name_ = other.name_;
    quality_ = other.quality_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public MetadataInfo Clone() {
    return new MetadataInfo(this);
  }

  /// <summary>Field number for the "platform" field.</summary>
  public const int PlatformFieldNumber = 1;
  private global::PlatformSupport platform_ = global::PlatformSupport.Unspecified;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::PlatformSupport Platform {
    get { return platform_; }
    set {
      platform_ = value;
    }
  }

  /// <summary>Field number for the "version" field.</summary>
  public const int VersionFieldNumber = 2;
  private int version_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Version {
    get { return version_; }
    set {
      version_ = value;
    }
  }

  /// <summary>Field number for the "name" field.</summary>
  public const int NameFieldNumber = 3;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "quality" field.</summary>
  public const int QualityFieldNumber = 4;
  private string quality_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Quality {
    get { return quality_; }
    set {
      quality_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as MetadataInfo);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(MetadataInfo other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Platform != other.Platform) return false;
    if (Version != other.Version) return false;
    if (Name != other.Name) return false;
    if (Quality != other.Quality) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Platform != global::PlatformSupport.Unspecified) hash ^= Platform.GetHashCode();
    if (Version != 0) hash ^= Version.GetHashCode();
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (Quality.Length != 0) hash ^= Quality.GetHashCode();
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
    if (Platform != global::PlatformSupport.Unspecified) {
      output.WriteRawTag(8);
      output.WriteEnum((int) Platform);
    }
    if (Version != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Version);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Name);
    }
    if (Quality.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Quality);
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
    if (Platform != global::PlatformSupport.Unspecified) {
      output.WriteRawTag(8);
      output.WriteEnum((int) Platform);
    }
    if (Version != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Version);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Name);
    }
    if (Quality.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Quality);
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
    if (Platform != global::PlatformSupport.Unspecified) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Platform);
    }
    if (Version != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Version);
    }
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (Quality.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Quality);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(MetadataInfo other) {
    if (other == null) {
      return;
    }
    if (other.Platform != global::PlatformSupport.Unspecified) {
      Platform = other.Platform;
    }
    if (other.Version != 0) {
      Version = other.Version;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.Quality.Length != 0) {
      Quality = other.Quality;
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
        case 8: {
          Platform = (global::PlatformSupport) input.ReadEnum();
          break;
        }
        case 16: {
          Version = input.ReadInt32();
          break;
        }
        case 26: {
          Name = input.ReadString();
          break;
        }
        case 34: {
          Quality = input.ReadString();
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
        case 8: {
          Platform = (global::PlatformSupport) input.ReadEnum();
          break;
        }
        case 16: {
          Version = input.ReadInt32();
          break;
        }
        case 26: {
          Name = input.ReadString();
          break;
        }
        case 34: {
          Quality = input.ReadString();
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class BinaryFile : pb::IMessage<BinaryFile>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<BinaryFile> _parser = new pb::MessageParser<BinaryFile>(() => new BinaryFile());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<BinaryFile> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::MetadataCommonReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BinaryFile() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BinaryFile(BinaryFile other) : this() {
    data_ = other.data_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BinaryFile Clone() {
    return new BinaryFile(this);
  }

  /// <summary>Field number for the "data" field.</summary>
  public const int DataFieldNumber = 1;
  private pb::ByteString data_ = pb::ByteString.Empty;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pb::ByteString Data {
    get { return data_; }
    set {
      data_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as BinaryFile);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(BinaryFile other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Data != other.Data) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Data.Length != 0) hash ^= Data.GetHashCode();
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
    if (Data.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Data);
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
    if (Data.Length != 0) {
      output.WriteRawTag(10);
      output.WriteBytes(Data);
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
    if (Data.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(Data);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(BinaryFile other) {
    if (other == null) {
      return;
    }
    if (other.Data.Length != 0) {
      Data = other.Data;
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
          Data = input.ReadBytes();
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
          Data = input.ReadBytes();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
