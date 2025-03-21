// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: domain/user_authentication_provider.proto

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

type AuthenticationProvider int32

const (
	AuthenticationProvider_PROVIDER_UNSPECIFIED AuthenticationProvider = 0
	AuthenticationProvider_INTERNAL             AuthenticationProvider = 1
	AuthenticationProvider_GOOGLE               AuthenticationProvider = 2
	AuthenticationProvider_META_MASK            AuthenticationProvider = 3
	AuthenticationProvider_BINANCE_CHAIN_WALLET AuthenticationProvider = 4
	AuthenticationProvider_FACEBOOK             AuthenticationProvider = 5
)

// Enum value maps for AuthenticationProvider.
var (
	AuthenticationProvider_name = map[int32]string{
		0: "PROVIDER_UNSPECIFIED",
		1: "INTERNAL",
		2: "GOOGLE",
		3: "META_MASK",
		4: "BINANCE_CHAIN_WALLET",
		5: "FACEBOOK",
	}
	AuthenticationProvider_value = map[string]int32{
		"PROVIDER_UNSPECIFIED": 0,
		"INTERNAL":             1,
		"GOOGLE":               2,
		"META_MASK":            3,
		"BINANCE_CHAIN_WALLET": 4,
		"FACEBOOK":             5,
	}
)

func (x AuthenticationProvider) Enum() *AuthenticationProvider {
	p := new(AuthenticationProvider)
	*p = x
	return p
}

func (x AuthenticationProvider) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (AuthenticationProvider) Descriptor() protoreflect.EnumDescriptor {
	return file_domain_user_authentication_provider_proto_enumTypes[0].Descriptor()
}

func (AuthenticationProvider) Type() protoreflect.EnumType {
	return &file_domain_user_authentication_provider_proto_enumTypes[0]
}

func (x AuthenticationProvider) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use AuthenticationProvider.Descriptor instead.
func (AuthenticationProvider) EnumDescriptor() ([]byte, []int) {
	return file_domain_user_authentication_provider_proto_rawDescGZIP(), []int{0}
}

var File_domain_user_authentication_provider_proto protoreflect.FileDescriptor

var file_domain_user_authentication_provider_proto_rawDesc = []byte{
	0x0a, 0x29, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x61, 0x75,
	0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x5f, 0x70, 0x72, 0x6f,
	0x76, 0x69, 0x64, 0x65, 0x72, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2a, 0x83, 0x01, 0x0a, 0x16,
	0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x50, 0x72,
	0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x12, 0x18, 0x0a, 0x14, 0x50, 0x52, 0x4f, 0x56, 0x49, 0x44,
	0x45, 0x52, 0x5f, 0x55, 0x4e, 0x53, 0x50, 0x45, 0x43, 0x49, 0x46, 0x49, 0x45, 0x44, 0x10, 0x00,
	0x12, 0x0c, 0x0a, 0x08, 0x49, 0x4e, 0x54, 0x45, 0x52, 0x4e, 0x41, 0x4c, 0x10, 0x01, 0x12, 0x0a,
	0x0a, 0x06, 0x47, 0x4f, 0x4f, 0x47, 0x4c, 0x45, 0x10, 0x02, 0x12, 0x0d, 0x0a, 0x09, 0x4d, 0x45,
	0x54, 0x41, 0x5f, 0x4d, 0x41, 0x53, 0x4b, 0x10, 0x03, 0x12, 0x18, 0x0a, 0x14, 0x42, 0x49, 0x4e,
	0x41, 0x4e, 0x43, 0x45, 0x5f, 0x43, 0x48, 0x41, 0x49, 0x4e, 0x5f, 0x57, 0x41, 0x4c, 0x4c, 0x45,
	0x54, 0x10, 0x04, 0x12, 0x0c, 0x0a, 0x08, 0x46, 0x41, 0x43, 0x45, 0x42, 0x4f, 0x4f, 0x4b, 0x10,
	0x05, 0x42, 0xa1, 0x01, 0x0a, 0x1e, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72,
	0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x64, 0x6f,
	0x6d, 0x61, 0x69, 0x6e, 0x42, 0x1b, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x50, 0x72, 0x6f, 0x74,
	0x6f, 0x50, 0x01, 0x5a, 0x42, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f,
	0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e,
	0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f,
	0x72, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69,
	0x2f, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0xaa, 0x02, 0x1b, 0x4d, 0x75, 0x7a, 0x69, 0x76, 0x65,
	0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x2e, 0x44,
	0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_domain_user_authentication_provider_proto_rawDescOnce sync.Once
	file_domain_user_authentication_provider_proto_rawDescData = file_domain_user_authentication_provider_proto_rawDesc
)

func file_domain_user_authentication_provider_proto_rawDescGZIP() []byte {
	file_domain_user_authentication_provider_proto_rawDescOnce.Do(func() {
		file_domain_user_authentication_provider_proto_rawDescData = protoimpl.X.CompressGZIP(file_domain_user_authentication_provider_proto_rawDescData)
	})
	return file_domain_user_authentication_provider_proto_rawDescData
}

var file_domain_user_authentication_provider_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_domain_user_authentication_provider_proto_goTypes = []interface{}{
	(AuthenticationProvider)(0), // 0: AuthenticationProvider
}
var file_domain_user_authentication_provider_proto_depIdxs = []int32{
	0, // [0:0] is the sub-list for method output_type
	0, // [0:0] is the sub-list for method input_type
	0, // [0:0] is the sub-list for extension type_name
	0, // [0:0] is the sub-list for extension extendee
	0, // [0:0] is the sub-list for field type_name
}

func init() { file_domain_user_authentication_provider_proto_init() }
func file_domain_user_authentication_provider_proto_init() {
	if File_domain_user_authentication_provider_proto != nil {
		return
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_domain_user_authentication_provider_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   0,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_domain_user_authentication_provider_proto_goTypes,
		DependencyIndexes: file_domain_user_authentication_provider_proto_depIdxs,
		EnumInfos:         file_domain_user_authentication_provider_proto_enumTypes,
	}.Build()
	File_domain_user_authentication_provider_proto = out.File
	file_domain_user_authentication_provider_proto_rawDesc = nil
	file_domain_user_authentication_provider_proto_goTypes = nil
	file_domain_user_authentication_provider_proto_depIdxs = nil
}
