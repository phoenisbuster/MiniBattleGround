// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: common/interact_common.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Muziverse.Proto.Item.Domain {

  /// <summary>Holder for reflection information generated from common/interact_common.proto</summary>
  public static partial class InteractCommonReflection {

    #region Descriptor
    /// <summary>File descriptor for common/interact_common.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static InteractCommonReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chxjb21tb24vaW50ZXJhY3RfY29tbW9uLnByb3RvKk8KCEludGVyYWN0EhgK",
            "FElOVEVSQUNUX1VOU1BFQ0lGSUVEEAASEwoPSU5URVJBQ1RfU0lNUExFEAES",
            "FAoQSU5URVJBQ1RfQURWQU5DRRACQpcBCh5pby5tdXppdmVyc2UucHJvdG8u",
            "aXRlbS5kb21haW5CDUludGVyYWN0UHJvdG9QAVpGZ2l0bGFiLmNvbS9tdXpp",
            "dmVyc2UvY29tbW9uL3Byb3RvZ29nZW5lcmF0b3IvaXRlbS1wcm90by1hcGkv",
            "cGtnL2NvbW1vbqoCG011eml2ZXJzZS5Qcm90by5JdGVtLkRvbWFpbmIGcHJv",
            "dG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Muziverse.Proto.Item.Domain.Interact), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum Interact {
    [pbr::OriginalName("INTERACT_UNSPECIFIED")] Unspecified = 0,
    [pbr::OriginalName("INTERACT_SIMPLE")] Simple = 1,
    [pbr::OriginalName("INTERACT_ADVANCE")] Advance = 2,
  }

  #endregion

}

#endregion Designer generated code
