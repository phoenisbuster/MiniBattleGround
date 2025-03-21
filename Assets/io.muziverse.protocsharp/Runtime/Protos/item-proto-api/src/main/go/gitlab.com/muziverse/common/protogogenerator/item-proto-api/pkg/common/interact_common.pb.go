// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: common/interact_common.proto

package common

import (
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

type Interact int32

const (
	Interact_INTERACT_UNSPECIFIED Interact = 0
	Interact_INTERACT_SIMPLE      Interact = 1
	Interact_INTERACT_ADVANCE     Interact = 2
)

// Enum value maps for Interact.
var (
	Interact_name = map[int32]string{
		0: "INTERACT_UNSPECIFIED",
		1: "INTERACT_SIMPLE",
		2: "INTERACT_ADVANCE",
	}
	Interact_value = map[string]int32{
		"INTERACT_UNSPECIFIED": 0,
		"INTERACT_SIMPLE":      1,
		"INTERACT_ADVANCE":     2,
	}
)

func (x Interact) Enum() *Interact {
	p := new(Interact)
	*p = x
	return p
}

func (x Interact) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (Interact) Descriptor() protoreflect.EnumDescriptor {
	return file_common_interact_common_proto_enumTypes[0].Descriptor()
}

func (Interact) Type() protoreflect.EnumType {
	return &file_common_interact_common_proto_enumTypes[0]
}

func (x Interact) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use Interact.Descriptor instead.
func (Interact) EnumDescriptor() ([]byte, []int) {
	return file_common_interact_common_proto_rawDescGZIP(), []int{0}
}

var File_common_interact_common_proto protoreflect.FileDescriptor

var file_common_interact_common_proto_rawDesc = []byte{
	0x0a, 0x1c, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x69, 0x6e, 0x74, 0x65, 0x72, 0x61, 0x63,
	0x74, 0x5f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2a, 0x4f,
	0x0a, 0x08, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x61, 0x63, 0x74, 0x12, 0x18, 0x0a, 0x14, 0x49, 0x4e,
	0x54, 0x45, 0x52, 0x41, 0x43, 0x54, 0x5f, 0x55, 0x4e, 0x53, 0x50, 0x45, 0x43, 0x49, 0x46, 0x49,
	0x45, 0x44, 0x10, 0x00, 0x12, 0x13, 0x0a, 0x0f, 0x49, 0x4e, 0x54, 0x45, 0x52, 0x41, 0x43, 0x54,
	0x5f, 0x53, 0x49, 0x4d, 0x50, 0x4c, 0x45, 0x10, 0x01, 0x12, 0x14, 0x0a, 0x10, 0x49, 0x4e, 0x54,
	0x45, 0x52, 0x41, 0x43, 0x54, 0x5f, 0x41, 0x44, 0x56, 0x41, 0x4e, 0x43, 0x45, 0x10, 0x02, 0x42,
	0x97, 0x01, 0x0a, 0x1e, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65,
	0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x69, 0x74, 0x65, 0x6d, 0x2e, 0x64, 0x6f, 0x6d, 0x61,
	0x69, 0x6e, 0x42, 0x0d, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x61, 0x63, 0x74, 0x50, 0x72, 0x6f, 0x74,
	0x6f, 0x50, 0x01, 0x5a, 0x46, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f,
	0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e,
	0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f,
	0x72, 0x2f, 0x69, 0x74, 0x65, 0x6d, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69,
	0x2f, 0x70, 0x6b, 0x67, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0xaa, 0x02, 0x1b, 0x4d, 0x75,
	0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x49, 0x74,
	0x65, 0x6d, 0x2e, 0x44, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f,
	0x33,
}

var (
	file_common_interact_common_proto_rawDescOnce sync.Once
	file_common_interact_common_proto_rawDescData = file_common_interact_common_proto_rawDesc
)

func file_common_interact_common_proto_rawDescGZIP() []byte {
	file_common_interact_common_proto_rawDescOnce.Do(func() {
		file_common_interact_common_proto_rawDescData = protoimpl.X.CompressGZIP(file_common_interact_common_proto_rawDescData)
	})
	return file_common_interact_common_proto_rawDescData
}

var file_common_interact_common_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_common_interact_common_proto_goTypes = []interface{}{
	(Interact)(0), // 0: Interact
}
var file_common_interact_common_proto_depIdxs = []int32{
	0, // [0:0] is the sub-list for method output_type
	0, // [0:0] is the sub-list for method input_type
	0, // [0:0] is the sub-list for extension type_name
	0, // [0:0] is the sub-list for extension extendee
	0, // [0:0] is the sub-list for field type_name
}

func init() { file_common_interact_common_proto_init() }
func file_common_interact_common_proto_init() {
	if File_common_interact_common_proto != nil {
		return
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_common_interact_common_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   0,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_common_interact_common_proto_goTypes,
		DependencyIndexes: file_common_interact_common_proto_depIdxs,
		EnumInfos:         file_common_interact_common_proto_enumTypes,
	}.Build()
	File_common_interact_common_proto = out.File
	file_common_interact_common_proto_rawDesc = nil
	file_common_interact_common_proto_goTypes = nil
	file_common_interact_common_proto_depIdxs = nil
}
