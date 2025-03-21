// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: game_content_island.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Muziverse.Proto.GameContent.Api.Island {
  public static partial class GameContentIslandService
  {
    static readonly string __ServiceName = "gamecontent.island.GameContentIslandService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest> __Marshaller_gamecontent_island_GameContentIslandCreateRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest> __Marshaller_gamecontent_island_GameContentIslandUpdateRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.StringValue> __Marshaller_google_protobuf_StringValue = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.StringValue.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> __Marshaller_GameContentIslandModel = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse> __Marshaller_gamecontent_island_GameIslandListResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest> __Marshaller_gamecontent_island_GameContentIslandChangeStatusRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_CreateSingleIsland = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateSingleIsland",
        __Marshaller_gamecontent_island_GameContentIslandCreateRequest,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_UpdateSingleIsland = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateSingleIsland",
        __Marshaller_gamecontent_island_GameContentIslandUpdateRequest,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty> __Method_DeleteSingleIslandByCode = new grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteSingleIslandByCode",
        __Marshaller_google_protobuf_StringValue,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> __Method_GetSingleIslandByCode = new grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSingleIslandByCode",
        __Marshaller_google_protobuf_StringValue,
        __Marshaller_GameContentIslandModel);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse> __Method_GetAllIslands = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllIslands",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_gamecontent_island_GameIslandListResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> __Method_ChangeIslandStatusByCode = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ChangeIslandStatusByCode",
        __Marshaller_gamecontent_island_GameContentIslandChangeStatusRequest,
        __Marshaller_GameContentIslandModel);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GameContentIslandService</summary>
    [grpc::BindServiceMethod(typeof(GameContentIslandService), "BindService")]
    public abstract partial class GameContentIslandServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> CreateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> UpdateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> DeleteSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> GetSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse> GetAllIslands(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> ChangeIslandStatusByCode(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GameContentIslandService</summary>
    public partial class GameContentIslandServiceClient : grpc::ClientBase<GameContentIslandServiceClient>
    {
      /// <summary>Creates a new client for GameContentIslandService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameContentIslandServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GameContentIslandService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameContentIslandServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameContentIslandServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameContentIslandServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty CreateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSingleIsland(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty CreateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateSingleIsland, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateSingleIslandAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSingleIslandAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateSingleIslandAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateSingleIsland, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty UpdateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateSingleIsland(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty UpdateSingleIsland(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UpdateSingleIsland, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateSingleIslandAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateSingleIslandAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateSingleIslandAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UpdateSingleIsland, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteSingleIslandByCode(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteSingleIslandByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteSingleIslandByCodeAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteSingleIslandByCodeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteSingleIslandByCodeAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteSingleIslandByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel GetSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleIslandByCode(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel GetSingleIslandByCode(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSingleIslandByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> GetSingleIslandByCodeAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleIslandByCodeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> GetSingleIslandByCodeAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSingleIslandByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse GetAllIslands(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllIslands(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse GetAllIslands(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllIslands, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse> GetAllIslandsAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllIslandsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse> GetAllIslandsAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllIslands, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel ChangeIslandStatusByCode(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ChangeIslandStatusByCode(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel ChangeIslandStatusByCode(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ChangeIslandStatusByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> ChangeIslandStatusByCodeAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ChangeIslandStatusByCodeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel> ChangeIslandStatusByCodeAsync(global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ChangeIslandStatusByCode, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GameContentIslandServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GameContentIslandServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GameContentIslandServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateSingleIsland, serviceImpl.CreateSingleIsland)
          .AddMethod(__Method_UpdateSingleIsland, serviceImpl.UpdateSingleIsland)
          .AddMethod(__Method_DeleteSingleIslandByCode, serviceImpl.DeleteSingleIslandByCode)
          .AddMethod(__Method_GetSingleIslandByCode, serviceImpl.GetSingleIslandByCode)
          .AddMethod(__Method_GetAllIslands, serviceImpl.GetAllIslands)
          .AddMethod(__Method_ChangeIslandStatusByCode, serviceImpl.ChangeIslandStatusByCode).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameContentIslandServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateSingleIsland, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandCreateRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.CreateSingleIsland));
      serviceBinder.AddMethod(__Method_UpdateSingleIsland, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandUpdateRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.UpdateSingleIsland));
      serviceBinder.AddMethod(__Method_DeleteSingleIslandByCode, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.DeleteSingleIslandByCode));
      serviceBinder.AddMethod(__Method_GetSingleIslandByCode, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.StringValue, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel>(serviceImpl.GetSingleIslandByCode));
      serviceBinder.AddMethod(__Method_GetAllIslands, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Island.GameIslandListResponse>(serviceImpl.GetAllIslands));
      serviceBinder.AddMethod(__Method_ChangeIslandStatusByCode, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Island.GameContentIslandChangeStatusRequest, global::Muziverse.Proto.GameContent.Domain.GameContentIslandModel>(serviceImpl.ChangeIslandStatusByCode));
    }

  }
}
#endregion
