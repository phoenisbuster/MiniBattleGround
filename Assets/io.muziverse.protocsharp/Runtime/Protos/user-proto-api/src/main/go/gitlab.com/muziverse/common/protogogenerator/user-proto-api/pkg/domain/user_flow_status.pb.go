// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: domain/user_flow_status.proto

package domain

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

type UserFlowStatus int32

const (
	UserFlowStatus_UF_UNSPECIFIED     UserFlowStatus = 0
	UserFlowStatus_WAITING_ACTIVATION UserFlowStatus = 1
	UserFlowStatus_COMPLETED          UserFlowStatus = 2
	UserFlowStatus_CHALLENGE          UserFlowStatus = 3
)

// Enum value maps for UserFlowStatus.
var (
	UserFlowStatus_name = map[int32]string{
		0: "UF_UNSPECIFIED",
		1: "WAITING_ACTIVATION",
		2: "COMPLETED",
		3: "CHALLENGE",
	}
	UserFlowStatus_value = map[string]int32{
		"UF_UNSPECIFIED":     0,
		"WAITING_ACTIVATION": 1,
		"COMPLETED":          2,
		"CHALLENGE":          3,
	}
)

func (x UserFlowStatus) Enum() *UserFlowStatus {
	p := new(UserFlowStatus)
	*p = x
	return p
}

func (x UserFlowStatus) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (UserFlowStatus) Descriptor() protoreflect.EnumDescriptor {
	return file_domain_user_flow_status_proto_enumTypes[0].Descriptor()
}

func (UserFlowStatus) Type() protoreflect.EnumType {
	return &file_domain_user_flow_status_proto_enumTypes[0]
}

func (x UserFlowStatus) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use UserFlowStatus.Descriptor instead.
func (UserFlowStatus) EnumDescriptor() ([]byte, []int) {
	return file_domain_user_flow_status_proto_rawDescGZIP(), []int{0}
}

var File_domain_user_flow_status_proto protoreflect.FileDescriptor

var file_domain_user_flow_status_proto_rawDesc = []byte{
	0x0a, 0x1d, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x66, 0x6c,
	0x6f, 0x77, 0x5f, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2a,
	0x5a, 0x0a, 0x0e, 0x55, 0x73, 0x65, 0x72, 0x46, 0x6c, 0x6f, 0x77, 0x53, 0x74, 0x61, 0x74, 0x75,
	0x73, 0x12, 0x12, 0x0a, 0x0e, 0x55, 0x46, 0x5f, 0x55, 0x4e, 0x53, 0x50, 0x45, 0x43, 0x49, 0x46,
	0x49, 0x45, 0x44, 0x10, 0x00, 0x12, 0x16, 0x0a, 0x12, 0x57, 0x41, 0x49, 0x54, 0x49, 0x4e, 0x47,
	0x5f, 0x41, 0x43, 0x54, 0x49, 0x56, 0x41, 0x54, 0x49, 0x4f, 0x4e, 0x10, 0x01, 0x12, 0x0d, 0x0a,
	0x09, 0x43, 0x4f, 0x4d, 0x50, 0x4c, 0x45, 0x54, 0x45, 0x44, 0x10, 0x02, 0x12, 0x0d, 0x0a, 0x09,
	0x43, 0x48, 0x41, 0x4c, 0x4c, 0x45, 0x4e, 0x47, 0x45, 0x10, 0x03, 0x42, 0x9d, 0x01, 0x0a, 0x1e,
	0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x42, 0x13,
	0x55, 0x73, 0x65, 0x72, 0x46, 0x6c, 0x6f, 0x77, 0x53, 0x74, 0x61, 0x74, 0x75, 0x73, 0x50, 0x72,
	0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x46, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f,
	0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d,
	0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61,
	0x74, 0x6f, 0x72, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61,
	0x70, 0x69, 0x2f, 0x70, 0x6b, 0x67, 0x2f, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0xaa, 0x02, 0x1b,
	0x4d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e,
	0x55, 0x73, 0x65, 0x72, 0x2e, 0x44, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x33,
}

var (
	file_domain_user_flow_status_proto_rawDescOnce sync.Once
	file_domain_user_flow_status_proto_rawDescData = file_domain_user_flow_status_proto_rawDesc
)

func file_domain_user_flow_status_proto_rawDescGZIP() []byte {
	file_domain_user_flow_status_proto_rawDescOnce.Do(func() {
		file_domain_user_flow_status_proto_rawDescData = protoimpl.X.CompressGZIP(file_domain_user_flow_status_proto_rawDescData)
	})
	return file_domain_user_flow_status_proto_rawDescData
}

var file_domain_user_flow_status_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_domain_user_flow_status_proto_goTypes = []interface{}{
	(UserFlowStatus)(0), // 0: UserFlowStatus
}
var file_domain_user_flow_status_proto_depIdxs = []int32{
	0, // [0:0] is the sub-list for method output_type
	0, // [0:0] is the sub-list for method input_type
	0, // [0:0] is the sub-list for extension type_name
	0, // [0:0] is the sub-list for extension extendee
	0, // [0:0] is the sub-list for field type_name
}

func init() { file_domain_user_flow_status_proto_init() }
func file_domain_user_flow_status_proto_init() {
	if File_domain_user_flow_status_proto != nil {
		return
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_domain_user_flow_status_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   0,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_domain_user_flow_status_proto_goTypes,
		DependencyIndexes: file_domain_user_flow_status_proto_depIdxs,
		EnumInfos:         file_domain_user_flow_status_proto_enumTypes,
	}.Build()
	File_domain_user_flow_status_proto = out.File
	file_domain_user_flow_status_proto_rawDesc = nil
	file_domain_user_flow_status_proto_goTypes = nil
	file_domain_user_flow_status_proto_depIdxs = nil
}
