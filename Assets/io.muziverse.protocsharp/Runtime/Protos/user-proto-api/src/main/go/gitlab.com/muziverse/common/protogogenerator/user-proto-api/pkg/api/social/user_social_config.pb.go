// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: user_social_config.proto

package social

import (
	domain "gitlab.com/muziverse/common/protogogenerator/user-proto-api/pkg/domain"
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

type UserSocialInfoRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Provider domain.AuthenticationProvider `protobuf:"varint,1,opt,name=provider,proto3,enum=AuthenticationProvider" json:"provider,omitempty"`
}

func (x *UserSocialInfoRequest) Reset() {
	*x = UserSocialInfoRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_social_config_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *UserSocialInfoRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*UserSocialInfoRequest) ProtoMessage() {}

func (x *UserSocialInfoRequest) ProtoReflect() protoreflect.Message {
	mi := &file_user_social_config_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use UserSocialInfoRequest.ProtoReflect.Descriptor instead.
func (*UserSocialInfoRequest) Descriptor() ([]byte, []int) {
	return file_user_social_config_proto_rawDescGZIP(), []int{0}
}

func (x *UserSocialInfoRequest) GetProvider() domain.AuthenticationProvider {
	if x != nil {
		return x.Provider
	}
	return domain.AuthenticationProvider(0)
}

type UserSocialInfoResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	ClientId    string `protobuf:"bytes,1,opt,name=client_id,json=clientId,proto3" json:"client_id,omitempty"`
	RedirectUrl string `protobuf:"bytes,2,opt,name=redirect_url,json=redirectUrl,proto3" json:"redirect_url,omitempty"`
}

func (x *UserSocialInfoResponse) Reset() {
	*x = UserSocialInfoResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_social_config_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *UserSocialInfoResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*UserSocialInfoResponse) ProtoMessage() {}

func (x *UserSocialInfoResponse) ProtoReflect() protoreflect.Message {
	mi := &file_user_social_config_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use UserSocialInfoResponse.ProtoReflect.Descriptor instead.
func (*UserSocialInfoResponse) Descriptor() ([]byte, []int) {
	return file_user_social_config_proto_rawDescGZIP(), []int{1}
}

func (x *UserSocialInfoResponse) GetClientId() string {
	if x != nil {
		return x.ClientId
	}
	return ""
}

func (x *UserSocialInfoResponse) GetRedirectUrl() string {
	if x != nil {
		return x.RedirectUrl
	}
	return ""
}

var File_user_social_config_proto protoreflect.FileDescriptor

var file_user_social_config_proto_rawDesc = []byte{
	0x0a, 0x18, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x73, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x5f, 0x63, 0x6f,
	0x6e, 0x66, 0x69, 0x67, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x12, 0x0f, 0x69, 0x64, 0x65, 0x6e,
	0x74, 0x69, 0x74, 0x79, 0x2e, 0x73, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x1a, 0x29, 0x64, 0x6f, 0x6d,
	0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x61, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74,
	0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x5f, 0x70, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72,
	0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22, 0x4c, 0x0a, 0x15, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f,
	0x63, 0x69, 0x61, 0x6c, 0x49, 0x6e, 0x66, 0x6f, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12,
	0x33, 0x0a, 0x08, 0x70, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x18, 0x01, 0x20, 0x01, 0x28,
	0x0e, 0x32, 0x17, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x52, 0x08, 0x70, 0x72, 0x6f, 0x76,
	0x69, 0x64, 0x65, 0x72, 0x22, 0x58, 0x0a, 0x16, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f, 0x63, 0x69,
	0x61, 0x6c, 0x49, 0x6e, 0x66, 0x6f, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x1b,
	0x0a, 0x09, 0x63, 0x6c, 0x69, 0x65, 0x6e, 0x74, 0x5f, 0x69, 0x64, 0x18, 0x01, 0x20, 0x01, 0x28,
	0x09, 0x52, 0x08, 0x63, 0x6c, 0x69, 0x65, 0x6e, 0x74, 0x49, 0x64, 0x12, 0x21, 0x0a, 0x0c, 0x72,
	0x65, 0x64, 0x69, 0x72, 0x65, 0x63, 0x74, 0x5f, 0x75, 0x72, 0x6c, 0x18, 0x02, 0x20, 0x01, 0x28,
	0x09, 0x52, 0x0b, 0x72, 0x65, 0x64, 0x69, 0x72, 0x65, 0x63, 0x74, 0x55, 0x72, 0x6c, 0x32, 0x7a,
	0x0a, 0x10, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x43, 0x6f, 0x6e, 0x66,
	0x69, 0x67, 0x12, 0x66, 0x0a, 0x11, 0x47, 0x65, 0x74, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f, 0x63,
	0x69, 0x61, 0x6c, 0x49, 0x6e, 0x66, 0x6f, 0x12, 0x26, 0x2e, 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69,
	0x74, 0x79, 0x2e, 0x73, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f,
	0x63, 0x69, 0x61, 0x6c, 0x49, 0x6e, 0x66, 0x6f, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a,
	0x27, 0x2e, 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x74, 0x79, 0x2e, 0x73, 0x6f, 0x63, 0x69, 0x61,
	0x6c, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x49, 0x6e, 0x66, 0x6f,
	0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22, 0x00, 0x42, 0xab, 0x01, 0x0a, 0x22, 0x69,
	0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74,
	0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x61, 0x70, 0x69, 0x2e, 0x73, 0x6f, 0x63, 0x69, 0x61,
	0x6c, 0x42, 0x15, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x43, 0x6f, 0x6e,
	0x66, 0x69, 0x67, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x4a, 0x67, 0x69, 0x74, 0x6c,
	0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65,
	0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67,
	0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x2d, 0x70, 0x72,
	0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f, 0x70, 0x6b, 0x67, 0x2f, 0x61, 0x70, 0x69, 0x2f,
	0x73, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0xaa, 0x02, 0x1f, 0x4d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72,
	0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x2e, 0x41, 0x70,
	0x69, 0x2e, 0x53, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_user_social_config_proto_rawDescOnce sync.Once
	file_user_social_config_proto_rawDescData = file_user_social_config_proto_rawDesc
)

func file_user_social_config_proto_rawDescGZIP() []byte {
	file_user_social_config_proto_rawDescOnce.Do(func() {
		file_user_social_config_proto_rawDescData = protoimpl.X.CompressGZIP(file_user_social_config_proto_rawDescData)
	})
	return file_user_social_config_proto_rawDescData
}

var file_user_social_config_proto_msgTypes = make([]protoimpl.MessageInfo, 2)
var file_user_social_config_proto_goTypes = []interface{}{
	(*UserSocialInfoRequest)(nil),      // 0: identity.social.UserSocialInfoRequest
	(*UserSocialInfoResponse)(nil),     // 1: identity.social.UserSocialInfoResponse
	(domain.AuthenticationProvider)(0), // 2: AuthenticationProvider
}
var file_user_social_config_proto_depIdxs = []int32{
	2, // 0: identity.social.UserSocialInfoRequest.provider:type_name -> AuthenticationProvider
	0, // 1: identity.social.UserSocialConfig.GetUserSocialInfo:input_type -> identity.social.UserSocialInfoRequest
	1, // 2: identity.social.UserSocialConfig.GetUserSocialInfo:output_type -> identity.social.UserSocialInfoResponse
	2, // [2:3] is the sub-list for method output_type
	1, // [1:2] is the sub-list for method input_type
	1, // [1:1] is the sub-list for extension type_name
	1, // [1:1] is the sub-list for extension extendee
	0, // [0:1] is the sub-list for field type_name
}

func init() { file_user_social_config_proto_init() }
func file_user_social_config_proto_init() {
	if File_user_social_config_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_user_social_config_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*UserSocialInfoRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_user_social_config_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*UserSocialInfoResponse); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_user_social_config_proto_rawDesc,
			NumEnums:      0,
			NumMessages:   2,
			NumExtensions: 0,
			NumServices:   1,
		},
		GoTypes:           file_user_social_config_proto_goTypes,
		DependencyIndexes: file_user_social_config_proto_depIdxs,
		MessageInfos:      file_user_social_config_proto_msgTypes,
	}.Build()
	File_user_social_config_proto = out.File
	file_user_social_config_proto_rawDesc = nil
	file_user_social_config_proto_goTypes = nil
	file_user_social_config_proto_depIdxs = nil
}
