// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: domain/user_common.proto

package domain

import (
	wallet "gitlab.com/muziverse/common/protogogenerator/user-proto-api/wallet"
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

type AccessFlowResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Status                     UserFlowStatus            `protobuf:"varint,1,opt,name=status,proto3,enum=UserFlowStatus" json:"status,omitempty"`
	AccessToken                string                    `protobuf:"bytes,2,opt,name=access_token,json=accessToken,proto3" json:"access_token,omitempty"`
	RefreshToken               string                    `protobuf:"bytes,3,opt,name=refresh_token,json=refreshToken,proto3" json:"refresh_token,omitempty"`
	SignatureVerificationToken string                    `protobuf:"bytes,4,opt,name=signature_verification_token,json=signatureVerificationToken,proto3" json:"signature_verification_token,omitempty"`
	OtpVerificationToken       string                    `protobuf:"bytes,5,opt,name=otp_verification_token,json=otpVerificationToken,proto3" json:"otp_verification_token,omitempty"`
	Challenge                  *wallet.SecurityChallenge `protobuf:"bytes,6,opt,name=challenge,proto3" json:"challenge,omitempty"`
}

func (x *AccessFlowResponse) Reset() {
	*x = AccessFlowResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_user_common_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AccessFlowResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AccessFlowResponse) ProtoMessage() {}

func (x *AccessFlowResponse) ProtoReflect() protoreflect.Message {
	mi := &file_domain_user_common_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AccessFlowResponse.ProtoReflect.Descriptor instead.
func (*AccessFlowResponse) Descriptor() ([]byte, []int) {
	return file_domain_user_common_proto_rawDescGZIP(), []int{0}
}

func (x *AccessFlowResponse) GetStatus() UserFlowStatus {
	if x != nil {
		return x.Status
	}
	return UserFlowStatus_UF_UNSPECIFIED
}

func (x *AccessFlowResponse) GetAccessToken() string {
	if x != nil {
		return x.AccessToken
	}
	return ""
}

func (x *AccessFlowResponse) GetRefreshToken() string {
	if x != nil {
		return x.RefreshToken
	}
	return ""
}

func (x *AccessFlowResponse) GetSignatureVerificationToken() string {
	if x != nil {
		return x.SignatureVerificationToken
	}
	return ""
}

func (x *AccessFlowResponse) GetOtpVerificationToken() string {
	if x != nil {
		return x.OtpVerificationToken
	}
	return ""
}

func (x *AccessFlowResponse) GetChallenge() *wallet.SecurityChallenge {
	if x != nil {
		return x.Challenge
	}
	return nil
}

var File_domain_user_common_proto protoreflect.FileDescriptor

var file_domain_user_common_proto_rawDesc = []byte{
	0x0a, 0x18, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x63, 0x6f,
	0x6d, 0x6d, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x1d, 0x64, 0x6f, 0x6d, 0x61,
	0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x66, 0x6c, 0x6f, 0x77, 0x5f, 0x73, 0x74, 0x61,
	0x74, 0x75, 0x73, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x24, 0x77, 0x61, 0x6c, 0x6c, 0x65,
	0x74, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x73, 0x65, 0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x5f,
	0x63, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22,
	0xaf, 0x02, 0x0a, 0x12, 0x41, 0x63, 0x63, 0x65, 0x73, 0x73, 0x46, 0x6c, 0x6f, 0x77, 0x52, 0x65,
	0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x27, 0x0a, 0x06, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73,
	0x18, 0x01, 0x20, 0x01, 0x28, 0x0e, 0x32, 0x0f, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x46, 0x6c, 0x6f,
	0x77, 0x53, 0x74, 0x61, 0x74, 0x75, 0x73, 0x52, 0x06, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73, 0x12,
	0x21, 0x0a, 0x0c, 0x61, 0x63, 0x63, 0x65, 0x73, 0x73, 0x5f, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18,
	0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0b, 0x61, 0x63, 0x63, 0x65, 0x73, 0x73, 0x54, 0x6f, 0x6b,
	0x65, 0x6e, 0x12, 0x23, 0x0a, 0x0d, 0x72, 0x65, 0x66, 0x72, 0x65, 0x73, 0x68, 0x5f, 0x74, 0x6f,
	0x6b, 0x65, 0x6e, 0x18, 0x03, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0c, 0x72, 0x65, 0x66, 0x72, 0x65,
	0x73, 0x68, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x40, 0x0a, 0x1c, 0x73, 0x69, 0x67, 0x6e, 0x61,
	0x74, 0x75, 0x72, 0x65, 0x5f, 0x76, 0x65, 0x72, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f,
	0x6e, 0x5f, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x04, 0x20, 0x01, 0x28, 0x09, 0x52, 0x1a, 0x73,
	0x69, 0x67, 0x6e, 0x61, 0x74, 0x75, 0x72, 0x65, 0x56, 0x65, 0x72, 0x69, 0x66, 0x69, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x34, 0x0a, 0x16, 0x6f, 0x74, 0x70,
	0x5f, 0x76, 0x65, 0x72, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x5f, 0x74, 0x6f,
	0x6b, 0x65, 0x6e, 0x18, 0x05, 0x20, 0x01, 0x28, 0x09, 0x52, 0x14, 0x6f, 0x74, 0x70, 0x56, 0x65,
	0x72, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12,
	0x30, 0x0a, 0x09, 0x63, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67, 0x65, 0x18, 0x06, 0x20, 0x01,
	0x28, 0x0b, 0x32, 0x12, 0x2e, 0x53, 0x65, 0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x43, 0x68, 0x61,
	0x6c, 0x6c, 0x65, 0x6e, 0x67, 0x65, 0x52, 0x09, 0x63, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67,
	0x65, 0x42, 0x91, 0x01, 0x0a, 0x1e, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72,
	0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x64, 0x6f,
	0x6d, 0x61, 0x69, 0x6e, 0x42, 0x0b, 0x43, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x74,
	0x6f, 0x50, 0x01, 0x5a, 0x42, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f,
	0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e,
	0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f,
	0x72, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69,
	0x2f, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0xaa, 0x02, 0x1b, 0x4d, 0x75, 0x7a, 0x69, 0x76, 0x65,
	0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x2e, 0x44,
	0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_domain_user_common_proto_rawDescOnce sync.Once
	file_domain_user_common_proto_rawDescData = file_domain_user_common_proto_rawDesc
)

func file_domain_user_common_proto_rawDescGZIP() []byte {
	file_domain_user_common_proto_rawDescOnce.Do(func() {
		file_domain_user_common_proto_rawDescData = protoimpl.X.CompressGZIP(file_domain_user_common_proto_rawDescData)
	})
	return file_domain_user_common_proto_rawDescData
}

var file_domain_user_common_proto_msgTypes = make([]protoimpl.MessageInfo, 1)
var file_domain_user_common_proto_goTypes = []interface{}{
	(*AccessFlowResponse)(nil),       // 0: AccessFlowResponse
	(UserFlowStatus)(0),              // 1: UserFlowStatus
	(*wallet.SecurityChallenge)(nil), // 2: SecurityChallenge
}
var file_domain_user_common_proto_depIdxs = []int32{
	1, // 0: AccessFlowResponse.status:type_name -> UserFlowStatus
	2, // 1: AccessFlowResponse.challenge:type_name -> SecurityChallenge
	2, // [2:2] is the sub-list for method output_type
	2, // [2:2] is the sub-list for method input_type
	2, // [2:2] is the sub-list for extension type_name
	2, // [2:2] is the sub-list for extension extendee
	0, // [0:2] is the sub-list for field type_name
}

func init() { file_domain_user_common_proto_init() }
func file_domain_user_common_proto_init() {
	if File_domain_user_common_proto != nil {
		return
	}
	file_domain_user_flow_status_proto_init()
	if !protoimpl.UnsafeEnabled {
		file_domain_user_common_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AccessFlowResponse); i {
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
			RawDescriptor: file_domain_user_common_proto_rawDesc,
			NumEnums:      0,
			NumMessages:   1,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_domain_user_common_proto_goTypes,
		DependencyIndexes: file_domain_user_common_proto_depIdxs,
		MessageInfos:      file_domain_user_common_proto_msgTypes,
	}.Build()
	File_domain_user_common_proto = out.File
	file_domain_user_common_proto_rawDesc = nil
	file_domain_user_common_proto_goTypes = nil
	file_domain_user_common_proto_depIdxs = nil
}
