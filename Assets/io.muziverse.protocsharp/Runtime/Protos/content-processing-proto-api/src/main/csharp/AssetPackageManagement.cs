// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: asset_package_management.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Contentprocessing.Assetpackagemanagement {

  /// <summary>Holder for reflection information generated from asset_package_management.proto</summary>
  public static partial class AssetPackageManagementReflection {

    #region Descriptor
    /// <summary>File descriptor for asset_package_management.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AssetPackageManagementReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5hc3NldF9wYWNrYWdlX21hbmFnZW1lbnQucHJvdG8SKGNvbnRlbnRwcm9j",
            "ZXNzaW5nLmFzc2V0cGFja2FnZW1hbmFnZW1lbnQaG2dvb2dsZS9wcm90b2J1",
            "Zi9lbXB0eS5wcm90bxodY29tbW9uL3Rvb2xfdHlwZV9jb21tb24ucHJvdG8i",
            "7QEKFlVwbG9hZEZpbGVBc3NldFJlcXVlc3QSYwoJaW5mb19maWxlGAEgASgL",
            "Mk4uY29udGVudHByb2Nlc3NpbmcuYXNzZXRwYWNrYWdlbWFuYWdlbWVudC5V",
            "cGxvYWRGaWxlQXNzZXRSZXF1ZXN0LkluZm9GaWxlQXNzZXRIABIOCgRkYXRh",
            "GAIgASgMSAAaUwoNSW5mb0ZpbGVBc3NldBIRCglmaWxlX25hbWUYASABKAkS",
            "HAoJdG9vbF90eXBlGAIgASgOMgkuVG9vbFR5cGUSEQoJY2hlY2tfc3VtGAMg",
            "ASgJQgkKB3JlcXVlc3QiKQoXVXBsb2FkRmlsZUFzc2V0UmVzcG9uc2USDgoG",
            "am9iX2lkGAEgASgDMrUBChZBc3NldFBhY2thZ2VNYW5hZ2VtZW50EpoBCg9V",
            "cGxvYWRGaWxlQXNzZXQSQC5jb250ZW50cHJvY2Vzc2luZy5hc3NldHBhY2th",
            "Z2VtYW5hZ2VtZW50LlVwbG9hZEZpbGVBc3NldFJlcXVlc3QaQS5jb250ZW50",
            "cHJvY2Vzc2luZy5hc3NldHBhY2thZ2VtYW5hZ2VtZW50LlVwbG9hZEZpbGVB",
            "c3NldFJlc3BvbnNlIgAoAULNAQpAaW8ubXV6aXZlcnNlLnByb3RvLmNvbnRu",
            "ZW50cHJvY2Vzc2luZy5hcGkuYXNzZXRwYWNrYWdlbWFuYWdlbWVudEIbQXNz",
            "ZXRQYWNrYWdlTWFuYWdlbWVudFByb3RvUAFaamdpdGxhYi5jb20vbXV6aXZl",
            "cnNlL2NvbW1vbi9wcm90b2dvZ2VuZXJhdG9yL2NvbnRlbnQtcHJvY2Vzc2lu",
            "Zy1wcm90by1hcGkvcGtnL2FwaS9hc3NldC1wYWNrYWdlLW1hbmFnZW1lbnRi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, global::ToolTypeCommonReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest), global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Parser, new[]{ "InfoFile", "Data" }, new[]{ "Request" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset), global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset.Parser, new[]{ "FileName", "ToolType", "CheckSum" }, null, null, null, null)}),
            new pbr::GeneratedClrTypeInfo(typeof(global::Contentprocessing.Assetpackagemanagement.UploadFileAssetResponse), global::Contentprocessing.Assetpackagemanagement.UploadFileAssetResponse.Parser, new[]{ "JobId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UploadFileAssetRequest : pb::IMessage<UploadFileAssetRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<UploadFileAssetRequest> _parser = new pb::MessageParser<UploadFileAssetRequest>(() => new UploadFileAssetRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<UploadFileAssetRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Contentprocessing.Assetpackagemanagement.AssetPackageManagementReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetRequest(UploadFileAssetRequest other) : this() {
      switch (other.RequestCase) {
        case RequestOneofCase.InfoFile:
          InfoFile = other.InfoFile.Clone();
          break;
        case RequestOneofCase.Data:
          Data = other.Data;
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetRequest Clone() {
      return new UploadFileAssetRequest(this);
    }

    /// <summary>Field number for the "info_file" field.</summary>
    public const int InfoFileFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset InfoFile {
      get { return requestCase_ == RequestOneofCase.InfoFile ? (global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset) request_ : null; }
      set {
        request_ = value;
        requestCase_ = value == null ? RequestOneofCase.None : RequestOneofCase.InfoFile;
      }
    }

    /// <summary>Field number for the "data" field.</summary>
    public const int DataFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Data {
      get { return requestCase_ == RequestOneofCase.Data ? (pb::ByteString) request_ : pb::ByteString.Empty; }
      set {
        request_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        requestCase_ = RequestOneofCase.Data;
      }
    }

    private object request_;
    /// <summary>Enum of possible cases for the "request" oneof.</summary>
    public enum RequestOneofCase {
      None = 0,
      InfoFile = 1,
      Data = 2,
    }
    private RequestOneofCase requestCase_ = RequestOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RequestOneofCase RequestCase {
      get { return requestCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearRequest() {
      requestCase_ = RequestOneofCase.None;
      request_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as UploadFileAssetRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(UploadFileAssetRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(InfoFile, other.InfoFile)) return false;
      if (Data != other.Data) return false;
      if (RequestCase != other.RequestCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (requestCase_ == RequestOneofCase.InfoFile) hash ^= InfoFile.GetHashCode();
      if (requestCase_ == RequestOneofCase.Data) hash ^= Data.GetHashCode();
      hash ^= (int) requestCase_;
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
      if (requestCase_ == RequestOneofCase.InfoFile) {
        output.WriteRawTag(10);
        output.WriteMessage(InfoFile);
      }
      if (requestCase_ == RequestOneofCase.Data) {
        output.WriteRawTag(18);
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
      if (requestCase_ == RequestOneofCase.InfoFile) {
        output.WriteRawTag(10);
        output.WriteMessage(InfoFile);
      }
      if (requestCase_ == RequestOneofCase.Data) {
        output.WriteRawTag(18);
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
      if (requestCase_ == RequestOneofCase.InfoFile) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(InfoFile);
      }
      if (requestCase_ == RequestOneofCase.Data) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Data);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(UploadFileAssetRequest other) {
      if (other == null) {
        return;
      }
      switch (other.RequestCase) {
        case RequestOneofCase.InfoFile:
          if (InfoFile == null) {
            InfoFile = new global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset();
          }
          InfoFile.MergeFrom(other.InfoFile);
          break;
        case RequestOneofCase.Data:
          Data = other.Data;
          break;
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
            global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset subBuilder = new global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset();
            if (requestCase_ == RequestOneofCase.InfoFile) {
              subBuilder.MergeFrom(InfoFile);
            }
            input.ReadMessage(subBuilder);
            InfoFile = subBuilder;
            break;
          }
          case 18: {
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
            global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset subBuilder = new global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Types.InfoFileAsset();
            if (requestCase_ == RequestOneofCase.InfoFile) {
              subBuilder.MergeFrom(InfoFile);
            }
            input.ReadMessage(subBuilder);
            InfoFile = subBuilder;
            break;
          }
          case 18: {
            Data = input.ReadBytes();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the UploadFileAssetRequest message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public sealed partial class InfoFileAsset : pb::IMessage<InfoFileAsset>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<InfoFileAsset> _parser = new pb::MessageParser<InfoFileAsset>(() => new InfoFileAsset());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<InfoFileAsset> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Contentprocessing.Assetpackagemanagement.UploadFileAssetRequest.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public InfoFileAsset() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public InfoFileAsset(InfoFileAsset other) : this() {
          fileName_ = other.fileName_;
          toolType_ = other.toolType_;
          checkSum_ = other.checkSum_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public InfoFileAsset Clone() {
          return new InfoFileAsset(this);
        }

        /// <summary>Field number for the "file_name" field.</summary>
        public const int FileNameFieldNumber = 1;
        private string fileName_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string FileName {
          get { return fileName_; }
          set {
            fileName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "tool_type" field.</summary>
        public const int ToolTypeFieldNumber = 2;
        private global::ToolType toolType_ = global::ToolType.ToolUnspecified;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public global::ToolType ToolType {
          get { return toolType_; }
          set {
            toolType_ = value;
          }
        }

        /// <summary>Field number for the "check_sum" field.</summary>
        public const int CheckSumFieldNumber = 3;
        private string checkSum_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string CheckSum {
          get { return checkSum_; }
          set {
            checkSum_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as InfoFileAsset);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(InfoFileAsset other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (FileName != other.FileName) return false;
          if (ToolType != other.ToolType) return false;
          if (CheckSum != other.CheckSum) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          if (FileName.Length != 0) hash ^= FileName.GetHashCode();
          if (ToolType != global::ToolType.ToolUnspecified) hash ^= ToolType.GetHashCode();
          if (CheckSum.Length != 0) hash ^= CheckSum.GetHashCode();
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
          if (FileName.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(FileName);
          }
          if (ToolType != global::ToolType.ToolUnspecified) {
            output.WriteRawTag(16);
            output.WriteEnum((int) ToolType);
          }
          if (CheckSum.Length != 0) {
            output.WriteRawTag(26);
            output.WriteString(CheckSum);
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
          if (FileName.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(FileName);
          }
          if (ToolType != global::ToolType.ToolUnspecified) {
            output.WriteRawTag(16);
            output.WriteEnum((int) ToolType);
          }
          if (CheckSum.Length != 0) {
            output.WriteRawTag(26);
            output.WriteString(CheckSum);
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
          if (FileName.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(FileName);
          }
          if (ToolType != global::ToolType.ToolUnspecified) {
            size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ToolType);
          }
          if (CheckSum.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(CheckSum);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(InfoFileAsset other) {
          if (other == null) {
            return;
          }
          if (other.FileName.Length != 0) {
            FileName = other.FileName;
          }
          if (other.ToolType != global::ToolType.ToolUnspecified) {
            ToolType = other.ToolType;
          }
          if (other.CheckSum.Length != 0) {
            CheckSum = other.CheckSum;
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
                FileName = input.ReadString();
                break;
              }
              case 16: {
                ToolType = (global::ToolType) input.ReadEnum();
                break;
              }
              case 26: {
                CheckSum = input.ReadString();
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
                FileName = input.ReadString();
                break;
              }
              case 16: {
                ToolType = (global::ToolType) input.ReadEnum();
                break;
              }
              case 26: {
                CheckSum = input.ReadString();
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

  public sealed partial class UploadFileAssetResponse : pb::IMessage<UploadFileAssetResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<UploadFileAssetResponse> _parser = new pb::MessageParser<UploadFileAssetResponse>(() => new UploadFileAssetResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<UploadFileAssetResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Contentprocessing.Assetpackagemanagement.AssetPackageManagementReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetResponse(UploadFileAssetResponse other) : this() {
      jobId_ = other.jobId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UploadFileAssetResponse Clone() {
      return new UploadFileAssetResponse(this);
    }

    /// <summary>Field number for the "job_id" field.</summary>
    public const int JobIdFieldNumber = 1;
    private long jobId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long JobId {
      get { return jobId_; }
      set {
        jobId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as UploadFileAssetResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(UploadFileAssetResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (JobId != other.JobId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (JobId != 0L) hash ^= JobId.GetHashCode();
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
      if (JobId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(JobId);
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
      if (JobId != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(JobId);
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
      if (JobId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(JobId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(UploadFileAssetResponse other) {
      if (other == null) {
        return;
      }
      if (other.JobId != 0L) {
        JobId = other.JobId;
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
            JobId = input.ReadInt64();
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
            JobId = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
